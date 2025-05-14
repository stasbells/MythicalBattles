using System;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class EquipmentItemsTypes : IShopItemVisitor
    {
        private List<Type> _itemTypes = new List<Type>();

        public IEnumerable<Type> GetTypes()
        {
            return _itemTypes;
        }
        
        public void Visit(IVisitorAcceptor item) => item.Accept(this);

        public void Visit(WeaponItem weaponItem)
        {
            Type itemType = weaponItem.GetType();
    
            TryAddTypeToList(itemType);
        }

        public void Visit(ArmorItem armorItem)
        {
            Type itemType = armorItem.GetType();
    
            TryAddTypeToList(itemType);
        }

        public void Visit(HelmetItem helmetItem)
        {
            Type itemType = helmetItem.GetType();
    
            TryAddTypeToList(itemType);
        }

        public void Visit(BootsItem bootsItem)
        {
            Type itemType = bootsItem.GetType();
    
            TryAddTypeToList(itemType);
        }

        public void Visit(NecklaceItem necklaceItem)
        {
            Type itemType = necklaceItem.GetType();
    
            TryAddTypeToList(itemType);
        }

        public void Visit(RingItem ringItem)
        {
            Type itemType = ringItem.GetType();

            TryAddTypeToList(itemType);
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
