using System;
using MythicalBattles.Assets.Scripts.Services.Data;
using MythicalBattles.Assets.Scripts.Shop.EquipmentShop;
using MythicalBattles.Assets.Scripts.UI.View.ScreenLevelSelector;

namespace MythicalBattles.Assets.Scripts.Services.ItemSelector
{
    public class ItemSelector : IItemSelector, IShopItemVisitor
    {
        private IPersistentData _persistentData;
        
        public ItemSelector(IPersistentData persistentData) => _persistentData = persistentData;
        
        public event Action SelectedItemChanged;
        
        public void Visit(IVisitorAcceptor item) => item.Accept(this);

        public void Visit(WeaponItem weaponItem)
        {
            _persistentData.PlayerData.SelectWeapon(weaponItem);
            
            SelectedItemChanged?.Invoke();
        }

        public void Visit(ArmorItem armorItem)
        {
            _persistentData.PlayerData.SelectArmor(armorItem);
            
            SelectedItemChanged?.Invoke();
        }

        public void Visit(HelmetItem helmetItem)
        {
            _persistentData.PlayerData.SelectHelmet(helmetItem);
            
            SelectedItemChanged?.Invoke();
        }

        public void Visit(BootsItem bootsItem)
        {
            _persistentData.PlayerData.SelectBoots(bootsItem);
            
            SelectedItemChanged?.Invoke();
        }

        public void Visit(NecklaceItem necklaceItem)
        {
            _persistentData.PlayerData.SelectNecklace(necklaceItem);
            
            SelectedItemChanged?.Invoke();
        }

        public void Visit(RingItem ringItem)
        {
            _persistentData.PlayerData.SelectRing(ringItem);
            
            SelectedItemChanged?.Invoke();
        }
    }
}
