using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Shop/EquipmentItems/WeaponItem")]
    public class WeaponItem : EquipmentItem
    {
        [SerializeField] private int _additionalDamage;

        private IPlayerStats _playerStats;
        
        public override void ApplyStats()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
            
            _playerStats.IncreaseDamage(_additionalDamage);
        }

        public override void CancelStats()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
            
            _playerStats.DecreaseDamage(_additionalDamage);
        }
    }
}
