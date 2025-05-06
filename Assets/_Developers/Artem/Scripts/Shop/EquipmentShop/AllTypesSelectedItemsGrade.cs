using UnityEngine;

namespace MythicalBattles
{
    public class AllTypesSelectedItemsGrade : IShopItemVisitor
    {
        private IPersistentData _persistentData;
        private EquipmentGrades _selectedGrade;
        
        public  AllTypesSelectedItemsGrade(IPersistentData persistentData) => _persistentData = persistentData;

        public EquipmentGrades GetGrade(ShopItem shopItem)
        {
            Visit(shopItem);
            
            return _selectedGrade;
        }

        //public void Visit(ShopItem shopItem) => Visit((dynamic) shopItem);

        public void Visit(ShopItem shopItem)
        {
            if (shopItem is WeaponItem weapon)
                Visit(weapon);
            else if (shopItem is ArmorItem armor)
                Visit(armor);
            else if (shopItem is HelmetItem helmet)
                Visit(helmet);
            else if (shopItem is BootsItem boots)
                Visit(boots);
            else if (shopItem is NecklaceItem necklace)
                Visit(necklace);
            else if (shopItem is RingItem ring)
                Visit(ring);
            else
                Debug.LogError("Unknown ShopItem type");
        }

        public void Visit(WeaponItem weaponItem)
        {
            _selectedGrade = _persistentData.PlayerData.GetSelectedWeapon().EquipmentGrade;
        }

        public void Visit(ArmorItem armorItem)
        {
            _selectedGrade = _persistentData.PlayerData.GetSelectedArmor().EquipmentGrade;
        }

        public void Visit(HelmetItem helmetItem)
        {
            _selectedGrade = _persistentData.PlayerData.GetSelectedHelmet().EquipmentGrade;
        }

        public void Visit(BootsItem bootsItem)
        {
            _selectedGrade = _persistentData.PlayerData.GetSelectedBoots().EquipmentGrade;
        }

        public void Visit(NecklaceItem necklaceItem)
        {
            _selectedGrade = _persistentData.PlayerData.GetSelectedNecklace().EquipmentGrade;
        }

        public void Visit(RingItem ringItem)
        {
            _selectedGrade = _persistentData.PlayerData.GetSelectedRing().EquipmentGrade;
        }
    }
}
