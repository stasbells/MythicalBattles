using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Shop/EquipmentItems/WeaponItem")]
    public class WeaponItem : EquipmentItem
    {
        [SerializeField] private float _additionalDamage;

        public float AdditionalDamage => _additionalDamage;
        public override string DisplayText => $"+{_additionalDamage} Damage";
        public override string TypeText => $"{EquipmentGrade} Bow";
        
        public override void Accept(IShopItemVisitor visitor)  => visitor.Visit(this);
    }
}
