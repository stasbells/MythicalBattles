namespace MythicalBattles.Shop.EquipmentShop
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
