using MythicalBattles.Assets.Scripts.UI.View.ScreenLevelSelector;

namespace MythicalBattles.Assets.Scripts.Shop.EquipmentShop
{
    public interface IShopItemVisitor
    {
        void Visit(IVisitorAcceptor item);
        void Visit(WeaponItem weaponItem);
        void Visit(ArmorItem armorItem);
        void Visit(HelmetItem helmetItem);
        void Visit(BootsItem bootsItem);
        void Visit(NecklaceItem necklaceItem);
        void Visit(RingItem ringItem);
    }
}
