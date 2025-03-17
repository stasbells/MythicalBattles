using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "HelmetItem", menuName = "Shop/EquipmentItems/HelmetItem")]
    public class HelmetItem : EquipmentItem
    {
        [SerializeField] private int _additionalHealth;

        private IPlayerStats _playerStats;
        
        public override void ApplyStats()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
            
            _playerStats.IncreaseMaxHealth(_additionalHealth);
        }

        public override void CancelStats()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
            
            _playerStats.DecreaseMaxHealth(_additionalHealth);
        }
    }
}
