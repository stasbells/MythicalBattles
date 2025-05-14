namespace MythicalBattles
{
    public class ItemSelector : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public ItemSelector(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(IVisitorAcceptor item) => item.Accept(this);

        public void Visit(WeaponItem weaponItem)
        {
            _persistentData.PlayerData.SelectWeapon(weaponItem);
        }

        public void Visit(ArmorItem armorItem)
        {
            _persistentData.PlayerData.SelectArmor(armorItem);
        }

        public void Visit(HelmetItem helmetItem)
        {
            _persistentData.PlayerData.SelectHelmet(helmetItem);
        }

        public void Visit(BootsItem bootsItem)
        {
            _persistentData.PlayerData.SelectBoots(bootsItem);
        }

        public void Visit(NecklaceItem necklaceItem)
        {
            _persistentData.PlayerData.SelectNecklace(necklaceItem);
        }

        public void Visit(RingItem ringItem)
        {
            _persistentData.PlayerData.SelectRing(ringItem);
        }
    }
}
