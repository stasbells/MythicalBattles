using MythicalBattles.Assets.Scripts.Shop.EquipmentShop;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenLevelSelector
{
    public interface IVisitorAcceptor
    {
        public void Accept(IShopItemVisitor visitor);
    }
}
