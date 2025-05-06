using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelSelector
{
    public class ScreenLevelSelectorBinder : ScreenBinder<ScreenLevelSelectorViewModel>
    {
        [SerializeField] private Button _goToSceneGameplayButton;
        [SerializeField] private Button _goToScreenMainMenuButton;

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
            ViewModel.RequestGoToSceneGameplay();
        }

        private void OnGoToScreenMainMenuButtonClicked()
        {
            ViewModel.RequestGoToScreenMainMenu();
        }
    }
}