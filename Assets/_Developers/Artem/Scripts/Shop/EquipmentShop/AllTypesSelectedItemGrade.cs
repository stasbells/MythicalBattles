namespace MythicalBattles
{
    public class AllTypesSelectedItemGrade : IShopItemVisitor
    {
        private IPersistentData _persistentData;
        private EquipmentGrades _selectedGrade;
        
        public  AllTypesSelectedItemGrade(IPersistentData persistentData) => _persistentData = persistentData;

        public EquipmentGrades GetGrade()
        {
            return _selectedGrade;
        }
        
        public void Visit(ShopItem shopItem) => Visit((dynamic) shopItem);

        public void Visit(WeaponItem weaponItem)
        {
            _selectedGrade = _persistentData.PlayerData.SelectedWeapon.EquipmentGrade;
        }

        public void Visit(ArmorItem armorItem)
        {
            _selectedGrade = _persistentData.PlayerData.SelectedArmor.EquipmentGrade;
        }

        public void Visit(HelmetItem helmetItem)
        {
            _selectedGrade = _persistentData.PlayerData.SelectedHelmet.EquipmentGrade;
        }

        public void Visit(BootsItem bootsItem)
        {
            _selectedGrade = _persistentData.PlayerData.SelectedBoots.EquipmentGrade;
        }

        public void Visit(NecklaceItem necklaceItem)
        {
            _selectedGrade = _persistentData.PlayerData.SelectedNecklace.EquipmentGrade;
        }

        public void Visit(RingItem ringItem)
        {
            _selectedGrade = _persistentData.PlayerData.SelectedRing.EquipmentGrade;
        }
    }
}
