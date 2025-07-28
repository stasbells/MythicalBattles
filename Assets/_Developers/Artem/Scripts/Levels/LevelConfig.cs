using Ami.BroAudio;
using MythicalBattles.Levels.EnemySpawner;
using UnityEngine;

namespace MythicalBattles.Levels
{
    [CreateAssetMenu(fileName = "newLevelConfig", menuName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject LevelDesignPrefab { get; private set; }
        [field: SerializeField] public WavesSpawner WavesSpawner { get; private set; }
        [field: SerializeField] public float BaseRewardMoney { get; private set; }
        [field: SerializeField] public float MaxScore { get; private set; }
        [field: SerializeField] public SoundID MusicTheme { get; private set; }
    }
}
