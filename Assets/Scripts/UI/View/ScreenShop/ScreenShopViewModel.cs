using MythicalBattles.Assets.Scripts.Shop;
using MythicalBattles.Assets.Scripts.UI.View.ScreenMainMenu;
using R3;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenShop
{
    public class ScreenShopViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;

        public ScreenShopViewModel(MainMenuUIManager mainMenuUIManager, ReactiveProperty<ShopPanel> shopPanel)
        {
            _uiManager = mainMenuUIManager;
            ShopPanel = shopPanel;
        }
        
        public ReactiveProperty<ShopPanel> ShopPanel { get; }
        public override string Name => "ScreenShop";

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