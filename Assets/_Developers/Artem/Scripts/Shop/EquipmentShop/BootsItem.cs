using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "BootsItem", menuName = "Shop/EquipmentItems/BootsItem")]
    public class BootsItem : EquipmentItem
    {
        [SerializeField] private float _additionalAttackSpeed;

        public float AdditionalAttackSpeed => _additionalAttackSpeed;
    }
}
