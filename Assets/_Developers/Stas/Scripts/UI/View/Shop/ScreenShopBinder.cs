using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.Shop
{
    public class ScreenShopBinder : ScreenBinder<ScreenShopViewModel>
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