using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu;
using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenShop
{
    public class ScreenShopViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;
        private readonly ReactiveProperty<ShopPanel> _shopPanel;

        public ReactiveProperty<ShopPanel> ShopPanel => _shopPanel;
        public override string Name => "ScreenShop";

        public ScreenShopViewModel(MainMenuUIManager mainMenuUIManager, ReactiveProperty<ShopPanel> shopPanel)
        {
            _uiManager = mainMenuUIManager;
            _shopPanel = shopPanel;
        }

        public void RequestGoToScreenMainMenu()
        {
            _uiManager.OpenScreenMainMenu();
        }

        public void RequestOpenPopupShopItem(ShopPanel shopPanel, ShopItemView shopItemView)
        {
            _uiManager.OpenPopupShopItem(shopPanel, shopItemView);
        }

        public void RequestOpenPopupEquipmentItem(InventoryItemView inventoryItemView)
        {
            _uiManager.OpenPopupEquipmentItem(inventoryItemView);
        }

        public void OnShopPanelChanged(ShopPanel newShopPanel, InventoryView inventoryView)
        {
            newShopPanel.SetViewModel(this);
            inventoryView.SetViewModel(this);
        }
    }
}