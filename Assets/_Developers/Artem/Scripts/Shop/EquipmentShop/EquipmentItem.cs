
using UnityEngine;

namespace MythicalBattles
{
    public class EquipmentItem : ShopItem
    {
        [SerializeField] private EquipmentGrades _equipmentGrade;
        
        public EquipmentGrades EquipmentGrade => _equipmentGrade;
    }
}
