using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Shop.EquipmentShop
{
    [CreateAssetMenu(fileName = "BootsItem", menuName = "Shop/EquipmentItems/BootsItem")]
    public class BootsItem : EquipmentItem
    {
        private const float ImprovementFactor = 100f;
        
        [SerializeField] private float _additionalAttackSpeed;

        public float AdditionalAttackSpeed => _additionalAttackSpeed;
        protected override string ItemName => Constants.Boots;
        protected override string ImprovementType => Constants.AttackSpeed;
        protected override string DisplayValue => $"{Prefix}{_additionalAttackSpeed * ImprovementFactor}%";

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);
    }
}