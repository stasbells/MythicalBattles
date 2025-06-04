using System;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelSelector
{
    public class ScreenLevelSelectorBinder : ScreenBinder<ScreenLevelSelectorViewModel>
    {
        [SerializeField] private Button _goToSceneGameplayButton;
        [SerializeField] private Button _goToScreenMainMenuButton;
        [SerializeField] private LevelSelectionCarousel _levelSelectionCarousel;
        
        private ILevelSelectionService _levelSelectionService;

        private void Construct()
        {
            _levelSelectionService = SceneManager.GetActiveScene().GetSceneContainer().Resolve<ILevelSelectionService>();
        }

        private void Awake()
        {
            Construct();
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
            
            ViewModel.RequestGoToSceneGameplay();
        }

        private void OnGoToScreenMainMenuButtonClicked()
        {
            ViewModel.RequestGoToScreenMainMenu();
        }
    }
}