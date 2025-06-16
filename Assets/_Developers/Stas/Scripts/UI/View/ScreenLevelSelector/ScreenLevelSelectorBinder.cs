using System;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelSelector
{
    public class ScreenLevelSelectorBinder : ScreenBinder<ScreenLevelSelectorViewModel>
    {
        [SerializeField] private Button _goToSceneGameplayButton;
        [SerializeField] private Button _goToScreenMainMenuButton;
        [SerializeField] private LevelSelectionCarousel _levelSelectionCarousel;
        [SerializeField] private TMP_Text _totalScore;

        private IPersistentData _persistentData;
        private ILevelSelectionService _levelSelectionService;

        private void Construct()
        {
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
            _levelSelectionService = SceneManager.GetActiveScene().GetSceneContainer().Resolve<ILevelSelectionService>();
        }

        private void Awake()
        {
            Construct();

            int totalScore = (int)_persistentData.GameProgressData.GetAllPoints();
            
            _totalScore.text = totalScore.ToString();
        }

        private void OnEnable()
        {
            _goToSceneGameplayButton.onClick.AddListener(OnGoToSceneGameplayButtonClicked);
            _goToScreenMainMenuButton.onClick.AddListener(OnGoToScreenMainMenuButtonClicked);
        }

        private void OnDisable()
        {
            _goToSceneGameplayButton.onClick.RemoveListener(OnGoToSceneGameplayButtonClicked);
            _goToScreenMainMenuButton.onClick.RemoveListener(OnGoToScreenMainMenuButtonClicked);
        }

        private void OnGoToSceneGameplayButtonClicked()
        {
            _levelSelectionService.SelectLevel(_levelSelectionCarousel.CurrentLevelNumber);

            // if (YandexGame.savesData.isFirstSession)
            // {
            //     ViewModel.RequestGoToTutorial();
            //     return;
            // }
            
            ViewModel.RequestGoToTutorial();
            
            // ViewModel.RequestGoToSceneGameplay();
        }

        private void OnGoToScreenMainMenuButtonClicked()
        {
            ViewModel.RequestGoToScreenMainMenu();
        }
    }
}