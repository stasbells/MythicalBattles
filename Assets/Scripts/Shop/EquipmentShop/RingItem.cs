using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Shop.EquipmentShop
{
    [CreateAssetMenu(fileName = "RingItem", menuName = "Shop/EquipmentItems/RingItem")]
    public class RingItem : EquipmentItem
    {
        private const float ImprovementFactor = 100f;
        
        [SerializeField] private float _additionalAttackSpeed;
        
        public float AdditionalAttackSpeed => _additionalAttackSpeed;
        protected override string ItemName => Constants.Ring;
        protected override string ImprovementType => Constants.AttackSpeed;
        protected override string DisplayValue => $"{Prefix}{_additionalAttackSpeed * ImprovementFactor}%";

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);
    }
}