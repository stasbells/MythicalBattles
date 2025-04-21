using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu
{
    public class ScreenMainMenuBinder : ScreenBinder<ScreenMainMenuViewModel>
    {
        [SerializeField] private Button _goToGameplayButton;

        private void OnEnable()
        {
            _goToGameplayButton.onClick.AddListener(OnGoToGameplayButtonClicked);
        }

        private void OnDisable()
        {
            _goToGameplayButton.onClick.RemoveListener(OnGoToGameplayButtonClicked);
        }

        private void OnGoToGameplayButtonClicked()
        {
            ViewModel.RequestGoToGameplay();
        }
    }
}