using System;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using Reflex.Attributes;
using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(LevelEndAlgorithm))]
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelConfig[] _levelConfigs;
        //[SerializeField] private Canvas _canvas;
        [SerializeField] private int _timeBetweenWaves = 6;
        [SerializeField] private float _enemyDyingTime = 3f;
    
        private ILevelSelectionService _levelSelection;
        private LevelEndAlgorithm _levelEndAlgorithm;
        private WavesSpawner _spawner;
        private int _currentLevelNumber;

        private Canvas _canvas;

        [Inject]
        private void Construct(ILevelSelectionService levelSelection)
        {
            _levelSelection = levelSelection;
        }
        
        private void Awake()
        {
            _canvas = FindObjectOfType<UIRootView>().GetComponentInChildren<Canvas>();

            _levelEndAlgorithm = GetComponent<LevelEndAlgorithm>();
            
            InitializeLevel();
        }

        private void OnDisable()
        {
            if (_spawner != null)
                _spawner.AllWavesCompleted -= OnAllWavesCompleted;
        }

        private void InitializeLevel()
        {
            _currentLevelNumber = _levelSelection.CurrentLevelNumber;
        
            if (_currentLevelNumber < 0 || _currentLevelNumber > _levelConfigs.Length)
            {
                Debug.LogError($"Invalid level index: {_currentLevelNumber}");
                return;
            }

            SpawnLevelDesign(_currentLevelNumber);
            InitializeWaveSpawner(_currentLevelNumber);
        }
        
        private void SpawnLevelDesign(int levelIndex)
        {
            var designPrefab = _levelConfigs[levelIndex - 1].LevelDesignPrefab;
        
            if (designPrefab == null)
            {
                Debug.LogError($"Interior prefab missing for level {levelIndex}");
                return;
            }

            _ = Instantiate(designPrefab);
        }

        private void InitializeWaveSpawner(int levelIndex)
        {
            var spawnerPrefab = _levelConfigs[levelIndex - 1].WavesSpawner;
        
            if (spawnerPrefab == null)
            {
                Debug.LogError($"WaveSpawner missing for level {levelIndex}");
                return;
            }

            GameObject spawnerObject = Instantiate(spawnerPrefab);

            if(spawnerObject.TryGetComponent(out WavesSpawner spawner) == false)
                throw new InvalidOperationException();
            
            _spawner = spawner;
            
            if(spawner.TryGetComponent(out WaveProgressHandler waveProgressHandler) == false)
                throw new InvalidOperationException();
            
            waveProgressHandler.Initialize(_canvas, _spawner.WavesCount, _timeBetweenWaves);
            
            _spawner.SetTimeBetweenWaves(_timeBetweenWaves);
            
            _spawner.SetEnemiesDyingTime(_enemyDyingTime);

            _spawner.AllWavesCompleted += OnAllWavesCompleted;
        }

        private void OnAllWavesCompleted()
        {
            float currentLevelBaseReward = _levelConfigs[_currentLevelNumber - 1].BaseRewardMoney;
            
            StartCoroutine(_levelEndAlgorithm.Run(_currentLevelNumber, currentLevelBaseReward));
        }
    }
}
