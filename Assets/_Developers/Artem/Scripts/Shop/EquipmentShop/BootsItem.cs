using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "BootsItem", menuName = "Shop/EquipmentItems/BootsItem")]
    public class BootsItem : EquipmentItem
    {
        [SerializeField] private float _additionalAttackSpeed;

        public float AdditionalAttackSpeed => _additionalAttackSpeed;
        public override string DisplayText => $"+{_additionalAttackSpeed*100f}% Attack speed";
        public override string TypeText => $"{EquipmentGrade} Boots";
        
        public override void Accept(IShopItemVisitor visitor)  => visitor.Visit(this);
    }
}