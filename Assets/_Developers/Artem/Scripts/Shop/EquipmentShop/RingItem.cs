using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "RingItem", menuName = "Shop/EquipmentItems/RingItem")]
    public class RingItem : EquipmentItem
    {
        [SerializeField] private float _additionalAttackSpeed;

        public float AdditionalAttackSpeed => _additionalAttackSpeed;
        public override string DisplayText => $"+{_additionalAttackSpeed*100f}% Attack speed";
        public override string TypeText => $"{EquipmentGrade} Ring";
        
        public override void Accept(IShopItemVisitor visitor)  => visitor.Visit(this);
    }
}
