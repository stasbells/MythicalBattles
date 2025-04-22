using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "newLevelConfig", menuName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private GameObject _levelDesignPrefab;
        [SerializeField] private WavesSpawner _waveSpawner;

        public GameObject LevelDesignPrefab => _levelDesignPrefab;
        public WavesSpawner WavesSpawner => _waveSpawner;
    }
}
