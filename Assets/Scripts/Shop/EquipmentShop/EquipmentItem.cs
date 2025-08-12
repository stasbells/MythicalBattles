using System;
using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Shop.EquipmentShop
{
    public abstract class EquipmentItem : ShopItem
    {
        private readonly Color SimpleColor = new (0.83f, 0.49f, 0.019f);
        private readonly Color CommonColor = new (0.015f, 0.8f, 0f);
        private readonly Color RareColor = new (0.37f, 0.56f, 1f);
        private readonly Color EpicColor = new (0.68f, 0.14f, 0.68f);
        private readonly Color LegendaryColor = new (0.85f, 0.32f, 0.12f);

        [SerializeField] private EquipmentGrades _equipmentGrade;
        
        public EquipmentGrades EquipmentGrade => _equipmentGrade;
        public override string DisplayText => $"{DisplayValue} {LanguagesDictionary.GetTranslation(ImprovementType)}";
        public string TypeText => $"{LanguagesDictionary.GetTranslation($"{EquipmentGrade} {ItemName}")}";
        protected abstract string ItemName { get; }
        protected abstract string ImprovementType { get; }
        protected abstract string DisplayValue { get; }
        protected string Prefix => "+";
        
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