using System.Collections;
using System.Collections.Generic;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class HealthBoost : Boost
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
