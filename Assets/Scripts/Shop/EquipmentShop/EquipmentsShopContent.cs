using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Shop.EquipmentShop
{
    [CreateAssetMenu(fileName = "EquipmentShopContent", menuName = "Shop/EquipmentShopContent")]
    public class EquipmentsShopContent : ScriptableObject
    {
        [SerializeField] private List<WeaponItem> _weaponItems;
        [SerializeField] private List<ArmorItem> _armorItems;
        [SerializeField] private List<HelmetItem> _helmetItems;
        [SerializeField] private List<BootsItem> _bootsItems;
        [SerializeField] private List<NecklaceItem> _necklaceItems;
        [SerializeField] private List<RingItem> _ringItems;
        
        private Dictionary<string, ShopItem> _itemsRegistry;

        public void InitializeRegistry()
        {
            CheckGradesDuplicates();
            
            _itemsRegistry = new Dictionary<string, ShopItem>();
        
            foreach (ShopItem item in GetItems())
            {
                if (_itemsRegistry.TryAdd(item.ItemID, item) == false)
                    throw new InvalidOperationException();
            }
        }
        
        public T GetItem<T>(string itemID)
            where T : ShopItem
        {
            if (string.IsNullOrEmpty(itemID))
                throw new InvalidOperationException();
            
            if (_itemsRegistry.TryGetValue(itemID, out ShopItem item))
            {
                if (item is T typedItem)
                    return typedItem;

                throw new InvalidOperationException();
            }
            
            return null;
        }

        public IEnumerable<ShopItem> GetItems()
        {
            return _weaponItems.Cast<ShopItem>()
                .Concat(_armorItems.Cast<ShopItem>())
                .Concat(_helmetItems.Cast<ShopItem>())
                .Concat(_bootsItems.Cast<ShopItem>())
                .Concat(_necklaceItems.Cast<ShopItem>())
                .Concat(_ringItems.Cast<ShopItem>());
        }

        private void CheckGradesDuplicates()
        {
            CheckForDuplicates(_weaponItems);
            CheckForDuplicates(_armorItems);
            CheckForDuplicates(_helmetItems);
            CheckForDuplicates(_bootsItems);
            CheckForDuplicates(_necklaceItems);
            CheckForDuplicates(_ringItems);
        }

        private void CheckForDuplicates(IEnumerable<EquipmentItem> items)
        {
            var itemsDuplicates = items.GroupBy(item => item.EquipmentGrade)
                    .Where(array => array.Count() > 1);
            
            if (itemsDuplicates.Any())
                throw new InvalidOperationException();
        }
    }
}
