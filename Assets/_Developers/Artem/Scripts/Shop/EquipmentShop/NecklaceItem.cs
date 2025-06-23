using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "NecklaceItem", menuName = "Shop/EquipmentItems/NecklaceItem")]
    public class NecklaceItem : EquipmentItem
    {
        [SerializeField] private float _additionalDamage;

        private readonly string _itemName = "Necklace";
        private readonly string _improvementType = "Damage";

        public float AdditionalDamage => _additionalDamage;
        public override string DisplayText => GetDisplayText();
        public override string TypeText => GetTypeText();

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);

        private string GetDisplayText()
        {
            return $"+{_additionalDamage} {LanguagesDictionary.GetTranslation(_improvementType)}";
        }

        private string GetTypeText()
        {
            return $"{LanguagesDictionary.GetTranslation($"{EquipmentGrade} {_itemName}")}";
        }
    }
}