using MythicalBattles.Assets.Scripts.Controllers.Enemies;

namespace MythicalBattles.Assets.Scripts.Levels.EnemySpawner
{
    [System.Serializable]
    public class EnemyWaveConfig
    {
        public Enemy EnemyPrefab;
        
        public int Count;
    }
}
