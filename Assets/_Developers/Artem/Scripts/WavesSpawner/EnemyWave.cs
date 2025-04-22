using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "EnemyWaves/EnemyWave")]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private EnemyWaveConfig[] enemyConfigs;

        [SerializeField] private float _powerMultiplier;
        
        public float PowerMultiplier => _powerMultiplier;

        public IEnumerable<EnemyWaveConfig> GetConfigs()
        {
            return enemyConfigs;
        }
    }
}
