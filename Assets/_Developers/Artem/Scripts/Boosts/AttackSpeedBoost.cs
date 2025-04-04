using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class AttackSpeedBoost : Boost
    {
        [SerializeField] private float _additionalAttackSpeed;

        private IPlayerStats _playerStats;

        protected override void Apply()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
            
            _playerStats.IncreaseAttackSpeed(_additionalAttackSpeed);
        }
    }
}
