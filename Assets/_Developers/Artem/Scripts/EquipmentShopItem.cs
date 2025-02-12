using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "EquipmentShopItem", menuName = "Shop/EquipmentShopItem")]
    public class EquipmentShopItem : ShopItem
    {
        [SerializeField] private EquipmentTypes _equipmentType;
        [SerializeField] private EquipmentGrades _equipmentGrade;

        public EquipmentTypes EquipmentType => _equipmentType;
        public EquipmentGrades EquipmentGrade => _equipmentGrade;
    }
}
