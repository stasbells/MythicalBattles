using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class MaxHealthBoost : Boost
    {
        [SerializeField] private int _additionalHealth;

        private IPlayerStats _playerStats;

        protected override void Apply()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
            
            _playerStats.IncreaseMaxHealth(_additionalHealth);
        }
    }
}
