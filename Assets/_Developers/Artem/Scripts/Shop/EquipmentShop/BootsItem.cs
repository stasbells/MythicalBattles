using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "BootsItem", menuName = "Shop/EquipmentItems/BootsItem")]
    public class BootsItem : EquipmentItem
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
