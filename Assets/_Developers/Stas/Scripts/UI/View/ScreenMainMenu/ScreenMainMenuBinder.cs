using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu
{
    public class ScreenMainMenuBinder : ScreenBinder<ScreenMainMenuViewModel>
    {
        [SerializeField] private Button _goToSceneGameplayButton;
        [SerializeField] private Button _goToScreenSettingsButton;

        private void OnEnable()
        {
            _goToSceneGameplayButton.onClick.AddListener(OnGoToSceneGameplayButtonClicked);
            _goToScreenSettingsButton.onClick.AddListener(OnGoToScreenSettingsButtonClicked);
        }

        private void OnDisable()
        {
            _goToSceneGameplayButton.onClick.RemoveListener(OnGoToSceneGameplayButtonClicked);
            _goToScreenSettingsButton.onClick.RemoveListener(OnGoToScreenSettingsButtonClicked);
        }

        private void OnGoToSceneGameplayButtonClicked()
        {
            ViewModel.RequestGoToSceneGameplay();
        }

        private void OnGoToScreenSettingsButtonClicked()
        {
            ViewModel.RequestGoToScreenSettings();
        }
    }
}