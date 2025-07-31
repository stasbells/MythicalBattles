using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Levels.EnemySpawner
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