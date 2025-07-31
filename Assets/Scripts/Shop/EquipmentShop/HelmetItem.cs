using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Shop.EquipmentShop
{
    [CreateAssetMenu(fileName = "HelmetItem", menuName = "Shop/EquipmentItems/HelmetItem")]
    public class HelmetItem : EquipmentItem
    {
        [SerializeField] private float _additionalHealth;

        public float AdditionalHealth => _additionalHealth;
        protected override string ItemName => Constants.Helmet;
        protected override string ImprovementType => Constants.Health;
        protected override string DisplayValue => $"{Prefix}{_additionalHealth}";

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);
    }
}