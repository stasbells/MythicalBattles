using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "newLevelConfig", menuName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private GameObject _levelDesignPrefab;
        [SerializeField] private GameObject _waveSpawner;

        public GameObject LevelDesignPrefab => _levelDesignPrefab;
        public GameObject WavesSpawner => _waveSpawner;
    }
}
