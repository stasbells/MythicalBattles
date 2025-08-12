using MythicalBattles.Assets.Scripts.Services.Data;
using MythicalBattles.Assets.Scripts.UI.View.ScreenLevelSelector;

namespace MythicalBattles.Assets.Scripts.Shop.EquipmentShop
{
    public class AllTypesSelectedItemsGrade : IShopItemVisitor
    {
        private IPersistentData _persistentData;
        private EquipmentGrades _selectedGrade;
        
        public AllTypesSelectedItemsGrade(IPersistentData persistentData) => _persistentData = persistentData;

        public EquipmentGrades GetGrade(ShopItem shopItem)
        {
            Visit(shopItem);
            
            return _selectedGrade;
        }

        public void Visit(IVisitorAcceptor item) => item.Accept(this);

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
