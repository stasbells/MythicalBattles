
using UnityEngine;

namespace MythicalBattles
{
    public abstract class EquipmentItem : ShopItem
    {
        [SerializeField] private EquipmentGrades _equipmentGrade;
        
        public EquipmentGrades EquipmentGrade => _equipmentGrade;

        public abstract void ApplyStats();
        public abstract void CancelStats();
    }
}
