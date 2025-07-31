using MythicalBattles.Assets.Scripts.UI.View.ScreenLevelSelector;
using System;
using System.Collections.Generic;

namespace MythicalBattles.Assets.Scripts.Shop.EquipmentShop
{
    public class EquipmentItemsTypes : IShopItemVisitor
    {
        private List<Type> _itemTypes = new();

        public IEnumerable<Type> GetTypes()
        {
            return _itemTypes;
        }
        
        public void Visit(IVisitorAcceptor item) => item.Accept(this);

        public void Visit(WeaponItem weaponItem)
        {
            TryAddTypeToList(weaponItem.GetType());
        }

        public void Visit(ArmorItem armorItem)
        {
            TryAddTypeToList(armorItem.GetType());
        }

        public void Visit(HelmetItem helmetItem)
        {
            TryAddTypeToList(helmetItem.GetType());
        }

        public void Visit(BootsItem bootsItem)
        {
            TryAddTypeToList(bootsItem.GetType());
        }

        public void Visit(NecklaceItem necklaceItem)
        {
            TryAddTypeToList(necklaceItem.GetType());
        }

        public void Visit(RingItem ringItem)
        {
            TryAddTypeToList(ringItem.GetType());
        }

        private bool TryAddTypeToList(Type itemType)
        {
            if (_itemTypes.Contains(itemType)) 
                return false;
            
            _itemTypes.Add(itemType);
            
            return true;
        }
    }
}
