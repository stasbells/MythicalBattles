using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "RingItem", menuName = "Shop/EquipmentItems/RingItem")]
    public class RingItem : EquipmentItem
    {
        [SerializeField] private float _additionalAttackSpeed;

        public float AdditionalAttackSpeed => _additionalAttackSpeed;
    }
}
