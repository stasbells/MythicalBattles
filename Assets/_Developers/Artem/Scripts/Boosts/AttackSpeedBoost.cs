using System.Collections;
using System.Collections.Generic;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class AttackSpeedBoost : Boost
    {
        [SerializeField] private int _additionalAttackSpeed;

        private IPlayerStats _playerStats;

        protected override void Apply()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
            
            _playerStats.IncreaseAttackSpeed(_additionalAttackSpeed);
        }
    }
}
