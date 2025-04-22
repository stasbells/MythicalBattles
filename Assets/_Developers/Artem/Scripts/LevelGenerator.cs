using Reflex.Attributes;
using UnityEngine;

namespace MythicalBattles
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelConfig[] _levelConfigs;
    
        private ILevelSelectionService _levelSelection;
    
        [Inject]
        private void Construct(ILevelSelectionService levelSelection)
        {
            _levelSelection = levelSelection;
        }
        
        private void Awake()
        {
            InitializeLevel();
        }
        
        private void InitializeLevel()
        {
            var currentLevel = _levelSelection.CurrentLevel;
        
            if (currentLevel < 0 || currentLevel >= _levelConfigs.Length)
            {
                Debug.LogError($"Invalid level index: {currentLevel}");
                return;
            }

            SpawnLevelInterior(currentLevel);
            InitializeWaveSpawner(currentLevel);
        }
        
        private void SpawnLevelInterior(int levelIndex)
        {
            var designPrefab = _levelConfigs[levelIndex].LevelDesignPrefab;
        
            if (designPrefab == null)
            {
                Debug.LogError($"Interior prefab missing for level {levelIndex}");
                return;
            }

            _ = Instantiate(designPrefab);
        }

        private void InitializeWaveSpawner(int levelIndex)
        {
            var spawnerPrefab = _levelConfigs[levelIndex].WavesSpawner;
        
            if (spawnerPrefab == null)
            {
                Debug.LogError($"WaveSpawner missing for level {levelIndex}");
                return;
            }

            _ = Instantiate(spawnerPrefab);
        }
    }
}
