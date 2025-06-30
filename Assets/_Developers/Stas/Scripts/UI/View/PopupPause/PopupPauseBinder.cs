using UnityEngine;
using UnityEngine.UI;
using YG;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupPause
{
    public class PopupPauseBinder : PopupBinder<PopupPauseViewModel>
    {
        [SerializeField] private Button _goToMainMenuButton;

        private void OnEnable()
        {
            _goToMainMenuButton.onClick.AddListener(OnGoToMainMenuButtonClicked);
            YandexGame.onShowWindowGame += OnShowWindowGame;

            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            _goToMainMenuButton.onClick.RemoveListener(OnGoToMainMenuButtonClicked);
            YandexGame.onShowWindowGame -= OnShowWindowGame;

            Time.timeScale = 1f;
        }

        private void OnGoToMainMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }

        private void OnShowWindowGame()
        {
            Time.timeScale = 0f;
        }
    }
}