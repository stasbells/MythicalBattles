using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles.Shop.EquipmentShop
{
    [CreateAssetMenu(fileName = "RingItem", menuName = "Shop/EquipmentItems/RingItem")]
    public class RingItem : EquipmentItem
    {
        [SerializeField] private float _additionalAttackSpeed;

        private readonly string _itemName = "Ring";
        private readonly string _improvementType = "Attack speed";
        private readonly float _improvementFactor = 100f;

        public float AdditionalAttackSpeed => _additionalAttackSpeed;
        public override string DisplayText => GetDisplayText();
        public override string TypeText => GetTypeText();

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);

        private string GetDisplayText()
        {
            return $"+{_additionalAttackSpeed * _improvementFactor}% {LanguagesDictionary.GetTranslation(_improvementType)}";
        }

        private string GetTypeText()
        {
            return $"{LanguagesDictionary.GetTranslation($"{EquipmentGrade} {_itemName}")}";
        }
    }
}