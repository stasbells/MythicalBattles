using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class DamageBoost : Boost
    {
        [SerializeField] private int _additionalDamage;

        private IPlayerStats _playerStats;

        protected override void Apply()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
            
            _playerStats.IncreaseDamage(_additionalDamage);
        }
    }
}
