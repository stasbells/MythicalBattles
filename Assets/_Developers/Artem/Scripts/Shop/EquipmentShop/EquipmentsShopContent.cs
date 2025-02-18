using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MythicalBattles
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

        public IEnumerable<EquipmentItem> GetEquipmentItems()
        {
            return _weaponItems.Cast<EquipmentItem>()
                .Concat(_armorItems.Cast<EquipmentItem>())
                .Concat(_helmetItems.Cast<EquipmentItem>())
                .Concat(_bootsItems.Cast<EquipmentItem>())
                .Concat(_necklaceItems.Cast<EquipmentItem>())
                .Concat(_ringItems.Cast<EquipmentItem>());
        }

        private void OnValidate()
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
            
            if(itemsDuplicates.Any())
                throw new InvalidOperationException();
        }
    }
}
