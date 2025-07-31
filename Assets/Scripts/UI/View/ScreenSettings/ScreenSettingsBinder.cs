using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenSettings
{
    public class ScreenSettingsBinder : ScreenBinder<ScreenSettingsViewModel>
    {
        [SerializeField] private Button _goToScreenMainMenuButton;

        private void OnEnable()
        {
            _goToScreenMainMenuButton.onClick.AddListener(OnGoToScreenMainMenuButtonClicked);
        }

        private void OnDisable()
        {
            _goToScreenMainMenuButton.onClick.RemoveListener(OnGoToScreenMainMenuButtonClicked);
        }

        private void OnGoToScreenMainMenuButtonClicked()
        {
            ViewModel.RequestGoToScreenMainMenu();
        }
    }
}