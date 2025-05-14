using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "ArmorItem", menuName = "Shop/EquipmentItems/ArmorItem")]
    public class ArmorItem : EquipmentItem
    {
        [SerializeField] private float _additionalHealth;

        public float AdditionalHealth => _additionalHealth;

        public override void Accept(IShopItemVisitor visitor)  => visitor.Visit(this);
    }
}
