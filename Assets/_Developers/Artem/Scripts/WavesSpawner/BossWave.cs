using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "BossWave", menuName = "EnemyWaves/BossWave")]
    public class BossWave : EnemyWave
    {
        [SerializeField] private EnemyWaveConfig _bossConfig;

        public EnemyWaveConfig GetBossConfig()
        {
            return _bossConfig;
        }
    }
}