using System;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        private void Construct()
        {
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
        }

        private void Awake()
        {
            Construct();
        }

        private void OnEnable()
        {
            ShowTime();
            
            ShowScore();
            
            ShowRewardMoney();
            
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
        }
        
        private void OnDisable()
        {
            _continueButton.onClick?.RemoveListener(OnContinueButtonClicked);
            _retryButton.onClick?.RemoveListener(OnRetryButtonClicked);
        }

        private void ShowTime()
        {
            
            Debug.Log(ViewModel.LevelPassTime);
            Debug.Log(ViewModel.BestTime);
            
            _completionTime.text = ConvertTimeToString(ViewModel.LevelPassTime);
            
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
                
                _bestTime.text = ConvertTimeToString(ViewModel.BestTime);
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
        
        private string ConvertTimeToString(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            
            int seconds = Mathf.FloorToInt(time % 60f);
            
            string formattedTime = $"{minutes:00}:{seconds:00}";
            
            return formattedTime;
        }

        private void OnContinueButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }
        
        private void OnRetryButtonClicked()
        {
            ViewModel.RequestToRestartLevel();
        }
    }
}