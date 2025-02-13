using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "EquipmentShopContent", menuName = "Shop/EquipmentShopContent")]
    public class EquipmentsShopContent : ScriptableObject
    {
        [SerializeField] private List<EquipmentShopItem> _equipmentItems;

        public IEnumerable<EquipmentShopItem> EquipmentItems => _equipmentItems;

        private void OnValidate()
        {
            CheckForDuplicates();
        }

        private void CheckForDuplicates()
        {
            var itemsDuplicates =
                _equipmentItems.GroupBy(item => new {item.EquipmentType, item.EquipmentGrade})
                    .Where(array => array.Count() > 1);
            
            if(itemsDuplicates.Any())
                throw new InvalidOperationException();
        }
    }
}
