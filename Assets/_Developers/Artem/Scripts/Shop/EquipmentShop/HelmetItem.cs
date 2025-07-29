using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles.Shop.EquipmentShop
{
    [CreateAssetMenu(fileName = "HelmetItem", menuName = "Shop/EquipmentItems/HelmetItem")]
    public class HelmetItem : EquipmentItem
    {
        private const string ItemName = "Helmet";
        private const string ImprovementType = "Health";
        private const string Plus = "+";
        
        [SerializeField] private float _additionalHealth;

        public float AdditionalHealth => _additionalHealth;
        public override string DisplayText => GetDisplayText();
        public override string TypeText => GetTypeText();

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);

        private string GetDisplayText()
        {
            return $"{Plus}{_additionalHealth} {LanguagesDictionary.GetTranslation(ImprovementType)}";
        }

        private string GetTypeText()
        {
            return $"{LanguagesDictionary.GetTranslation($"{EquipmentGrade} {ItemName}")}";
        }
    }
}