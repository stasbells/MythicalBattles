using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupB
{
    public class PopupShopItemViewModel : ScreenViewModel
    {
        private Observable<PopupShopItemViewModel> _buyItemRequested;
        private readonly Observable<ShopPanel> _shopPanel;

        public override string Name => "PopupShopItem";

        public Observable<PopupShopItemViewModel> BuyItemRequested => _buyItemRequested;

        public PopupShopItemViewModel(Observable<ShopPanel> shopPanel)
        {
            _shopPanel = shopPanel;
        }
    }
}