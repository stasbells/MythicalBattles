using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Shop/EquipmentItems/WeaponItem")]
    public class WeaponItem : EquipmentItem
    {
        [SerializeField] private float _additionalDamage;

        public float AdditionalDamage => _additionalDamage;
    }
}
