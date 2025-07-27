using MythicalBattles.Services.LevelCompletionStopwatch;
using MythicalBattles.Services.Wallet;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenDeath
{
    public class ScreenDeathBinder : ScreenBinder<ScreenDeathViewModel>
    {
        private const float MaxMoneyForTimeLife = 4000f;
        private const float MaxLifeTimeForReward = 600f;

        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _retryButton;
        [SerializeField] private TMP_Text _moneyRewardText;

        private ILevelCompletionStopwatch _levelCompletionStopwatch;
        private IWallet _wallet;
        private float _moneyReward;

        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _wallet = container.Resolve<IWallet>();
            _levelCompletionStopwatch = container.Resolve<ILevelCompletionStopwatch>();
        }

        private void Awake()
        {
            Construct();
        }

        private void OnEnable()
        {

            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _retryButton.onClick.AddListener(OnRetryButtonClicked);

            _levelCompletionStopwatch.Stop();

            ShowRewardMoney();

            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;

            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _retryButton.onClick.RemoveListener(OnRetryButtonClicked);
            YG2.onCloseInterAdv -= ViewModel.RequestToRestartLevel;
        }

        private void ShowRewardMoney()
        {
            if (MaxLifeTimeForReward < _levelCompletionStopwatch.ElapsedTime)
            {
                _moneyReward = MaxMoneyForTimeLife;
            }
            else
            {
                _moneyReward = MaxMoneyForTimeLife * _levelCompletionStopwatch.ElapsedTime / MaxLifeTimeForReward;
            }

            _wallet.AddCoins((int)_moneyReward);

            _moneyRewardText.text = ((int)_moneyReward).ToString();
        }

        private void OnMainMenuButtonClicked()
        {
            YG2.onCloseInterAdv += OnInterstitialAdClose;

            YG2.InterstitialAdvShow();
        }

        private void OnRetryButtonClicked()
        {
            YG2.onCloseInterAdv += ViewModel.RequestToRestartLevel;

            YG2.InterstitialAdvShow();
        }

        private void OnInterstitialAdClose()
        {
            ViewModel.RequestGoToMainMenu();

            YG2.onCloseInterAdv -= OnInterstitialAdClose;
        }
    }
}
