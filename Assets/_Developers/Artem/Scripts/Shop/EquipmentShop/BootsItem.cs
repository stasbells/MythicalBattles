using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "BootsItem", menuName = "Shop/EquipmentItems/BootsItem")]
    public class BootsItem : EquipmentItem
    {
        [SerializeField] private float _additionalAttackSpeed;

        private readonly string _itemName = "Boots";
        private readonly string _improvementType = "Attack speed";
        private readonly float _improvmentFactor = 100f;

        public float AdditionalAttackSpeed => _additionalAttackSpeed;
        public override string DisplayText => GetDisplayText();
        public override string TypeText => GetTypeText();

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);

        private string GetDisplayText()
        {
            return $"+{_additionalAttackSpeed * _improvmentFactor}% {LanguagesDictionary.GetTranslation(_improvementType)}";
        }

        private string GetTypeText()
        {
            return $"{LanguagesDictionary.GetTranslation($"{EquipmentGrade} {_itemName}")}";
        }
    }
}