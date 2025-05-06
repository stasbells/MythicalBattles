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
