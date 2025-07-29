using MythicalBattles.Shop;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupB
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