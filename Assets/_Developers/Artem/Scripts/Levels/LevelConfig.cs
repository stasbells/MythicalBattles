using Ami.BroAudio;
using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "newLevelConfig", menuName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject LevelDesignPrefab { get; private set; }
        [field: SerializeField] public GameObject WavesSpawner { get; private set; }
        [field: SerializeField] public float BaseRewardMoney { get; private set; }
        [field: SerializeField] public SoundID MusicTheme { get; private set; }
    }
}
