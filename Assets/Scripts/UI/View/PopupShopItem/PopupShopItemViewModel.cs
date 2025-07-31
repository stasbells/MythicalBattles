using MythicalBattles.Assets.Scripts.Shop;

namespace MythicalBattles.Assets.Scripts.UI.View.PopupShopItem
{
    public class PopupShopItemViewModel : ScreenViewModel
    {
        public override string Name => "PopupShopItem";
        public ShopItemView ShopItemView { get; }
        public ShopPanel ShopPanel { get; }

        public PopupShopItemViewModel(ShopPanel shopPanel, ShopItemView shopItemView)
        {
            ShopItemView = shopItemView;
            ShopPanel = shopPanel;
        }
    }
}