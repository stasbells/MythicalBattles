using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "NecklaceItem", menuName = "Shop/EquipmentItems/NecklaceItem")]
    public class NecklaceItem : EquipmentItem
    {
        [SerializeField] private float _additionalDamage;

        public float AdditionalDamage => _additionalDamage;
        
        public override void Accept(IShopItemVisitor visitor)  => visitor.Visit(this);
    }
}
