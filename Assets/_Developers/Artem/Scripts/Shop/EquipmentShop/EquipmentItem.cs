using System;
using UnityEngine;

namespace MythicalBattles
{
    public abstract class EquipmentItem : ShopItem
    {
        private readonly Color SimpleColor = new Color(0.46f, 0.19f, 0.16f);
        private readonly Color CommonColor = new Color(0.06f, 0.46f, 0.05f);
        private readonly Color RareColor = Color.blue;
        private readonly Color EpicColor = new Color(0.5f, 0, 0.5f);
        private readonly Color LegendaryColor = new Color(0.68f, 0.24f, 0.08f);
        
        [SerializeField] private EquipmentGrades _equipmentGrade;
        public EquipmentGrades EquipmentGrade => _equipmentGrade;
        public abstract string TypeText { get; }

        public Color GradeTextColor
        {
            get
            {
                return _equipmentGrade switch
                {
                    EquipmentGrades.Simple => SimpleColor,
                    EquipmentGrades.Common => CommonColor,
                    EquipmentGrades.Rare => RareColor,
                    EquipmentGrades.Epic => EpicColor,
                    EquipmentGrades.Legendary => LegendaryColor,
                    _ => throw new InvalidOperationException()
                };
            }
        }
    }
}