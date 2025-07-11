using System;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelComplete
{
    public class ScreenLevelCompleteBinder : ScreenBinder<ScreenLevelCompleteViewModel>
    {
        [SerializeField] private GameObject _moneyAwardView;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _retryButton;
        [SerializeField] private TMP_Text _moneyAward;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _completionTime;
        [SerializeField] private TMP_Text _bestTime;
        [SerializeField] private TMP_Text _newRecordText;
        [SerializeField] private TMP_Text _bestTimeText;
        
        private IPersistentData _persistentData;
        private ILevelSelectionService _levelSelectionService;
   
        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();
            
            _persistentData = container.Resolve<IPersistentData>();
            _levelSelectionService = container.Resolve<ILevelSelectionService>();
        }

        private void Awake()
        {
            Construct();
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
        }

        private void Start()
        {
            ShowTime();
            
            ShowScore();
            
            ShowRewardMoney();
        }

        private void OnDisable()
        {
            _continueButton.onClick?.RemoveListener(OnContinueButtonClicked);
            _retryButton.onClick?.RemoveListener(OnRetryButtonClicked);
        }

        private void ShowTime()
        {
            if(ViewModel == null)
                throw new InvalidOperationException();

            Debug.Log(ViewModel.LevelPassTime);
            Debug.Log(ViewModel.BestTime);
            
            _completionTime.text = TimeFormatter.GetTimeInString(ViewModel.LevelPassTime);
            
            if (Mathf.Approximately(ViewModel.LevelPassTime, ViewModel.BestTime))
            {
                _bestTime.gameObject.SetActive(false);
                _bestTimeText.gameObject.SetActive(false);
                _newRecordText.gameObject.SetActive(true);
            }
            else
            {
                _bestTime.gameObject.SetActive(true);
                _bestTimeText.gameObject.SetActive(true);
                _newRecordText.gameObject.SetActive(false);
                
                _bestTime.text = TimeFormatter.GetTimeInString(ViewModel.BestTime);
            }
        }

        private void ShowScore()
        {
            _score.text = ViewModel.Score.ToString();
        }

        private void ShowRewardMoney()
        {
            if (ViewModel.RewardMoney == 0)
                _moneyAwardView.SetActive(false);
            else
                _moneyAward.text = ViewModel.RewardMoney.ToString();
        }
        
        private void OnRetryButtonClicked()
        {
            ViewModel.RequestToRestartLevel();
        }

        private void OnContinueButtonClicked()
        {
            YG2.onCloseInterAdv += OnInterstitialAdClose;

            YG2.InterstitialAdvShow();
        }

        private void OnInterstitialAdClose()
        {
            if(_levelSelectionService.CurrentLevelNumber == _levelSelectionService.LastLevelNumber)
                ViewModel.RequestOpenScreenGameComplete();
            else
                ViewModel.RequestGoToMainMenu();

            YG2.onCloseInterAdv -= OnInterstitialAdClose;
        }
    }
}