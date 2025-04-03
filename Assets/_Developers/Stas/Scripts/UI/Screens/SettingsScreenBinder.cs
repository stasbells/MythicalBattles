using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.Screens
{
    public class SettingsScreenBinder : ScreenBinder<SettingsScreenViewModel>
    {
        [SerializeField] private Button _goToMenuButton;

        private void OnEnable()
        {
            _goToMenuButton.onClick.AddListener(OnGoToMenuButtonClicked);
        }

        private void OnDisable()
        {
            _goToMenuButton.onClick.RemoveListener(OnGoToMenuButtonClicked);
        }

        private void OnGoToMenuButtonClicked()
        {
            ViewModel.RequestGoToMenu();
        }
    }
}