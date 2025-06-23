using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "ArmorItem", menuName = "Shop/EquipmentItems/ArmorItem")]
    public class ArmorItem : EquipmentItem
    {
        [SerializeField] private float _additionalHealth;

        private readonly string _itemName = "Armor";
        private readonly string _improvementType = "Health";

        public float AdditionalHealth => _additionalHealth;
        public override string DisplayText => GetDisplayText();
        public override string TypeText => GetTypeText();

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);

        private string GetDisplayText()
        {
            return $"+{_additionalHealth} {LanguagesDictionary.GetTranslation(_improvementType)}";
        }

        private string GetTypeText()
        {
            return $"{LanguagesDictionary.GetTranslation($"{EquipmentGrade} {_itemName}")}";
        }
    }
}