using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ami.BroAudio;
using MythicalBattles.Assets.Scripts.Controllers.Boosts;
using MythicalBattles.Assets.Scripts.Controllers.Enemies;
using MythicalBattles.Assets.Scripts.Levels.WaveProgress;
using MythicalBattles.Assets.Scripts.Services.AudioPlayback;
using MythicalBattles.Assets.Scripts.Utils;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets.Scripts.Levels.EnemySpawner
{
    [RequireComponent(typeof(WaveProgressHandler))]
    public class WavesSpawner : MonoBehaviour
    {
        private const int HealDropEnemySerialNumber = 1;

        [SerializeField] private EnemyWave[] _waves;
        [SerializeField] private EnemySpawnPoints _enemySpawnPoints;
        [SerializeField] private BoostsStorage _boostsStorage;
        [SerializeField] private float _healDropPercentChance = 30f;

        private Dictionary<EnemyTypes, EnemyPool> _enemyPools = new Dictionary<EnemyTypes, EnemyPool>();
        private List<Vector3> _shuffledSpawnPoints = new List<Vector3>();
        private int _currentWaveNumber;
        private int _activeEnemiesCount;
        private int _timeBetweenWaves;
        private float _enemyDyingTime;
        private bool _isSpawning;
        private System.Random _random = new System.Random();
        private WaveProgressHandler _waveProgressHandler;
        private IAudioPlayback _audioPlayback;
        
        public event Action AllWavesCompleted;
        
        public int WavesCount => _waves.Length;

        private void Construct()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }
        
        private void Awake()
        {
            Construct();
            
            _waveProgressHandler = GetComponent<WaveProgressHandler>();
            
            InitializePools();
        }

        private void Start()
        {
            StartNextWave();
        }

        public void SetTimeBetweenWaves(int timeBetweenWaves)
        {
            if (timeBetweenWaves < 0)
                throw new InvalidOperationException();
            
            _timeBetweenWaves = timeBetweenWaves;
        }
        
        public void SetEnemiesDyingTime(float dyingTime)
        {
            if (dyingTime < 0)
                throw new InvalidOperationException();
            
            _enemyDyingTime = dyingTime;
        }
        
        private void InitializePools()
        {
            foreach (EnemyWave wave in _waves)
            {
                foreach (EnemyWaveConfig config in wave.GetConfigs())
                    UpdatePool(config);

                if (wave is BossWave bossWave)
                    UpdatePool(bossWave.GetBossConfig());
            }
        }

        private void UpdatePool(EnemyWaveConfig config)
        {
            if (_enemyPools.ContainsKey(config.EnemyPrefab.Type) == false)
            {
                _enemyPools.Add(config.EnemyPrefab.Type, new EnemyPool(config.EnemyPrefab, config.Count + 1, OnEnemyDead, transform));
            }
            else
            {
                _ = _enemyPools[config.EnemyPrefab.Type].TryUpdateSize(config.Count + 1);
            }
        }

        private void StartNextWave()
        {
            if (_currentWaveNumber >= _waves.Length)
            {
                AllWavesCompleted?.Invoke();
                return;
            }

            _currentWaveNumber++;
            
            StartCoroutine(SpawnWaveWithDelay());
        }

        private IEnumerator SpawnWaveWithDelay()
        {
            if (_currentWaveNumber > 1)
                yield return new WaitForSeconds(_timeBetweenWaves);

            SpawnWave(_waves[_currentWaveNumber - 1], _currentWaveNumber);
        }

        private void SpawnWave(EnemyWave wave, int waveNumber)
        {
            _activeEnemiesCount = 0;
            
            ShuffleSpawnPoints();

            foreach (EnemyWaveConfig config in wave.GetConfigs())
            {
                for (int i = 0; i < config.Count; i++)
                {
                    Enemy enemy = _enemyPools[config.EnemyPrefab.Type].GetEnemy();
                    
                    enemy.transform.position = GetSpawnPosition();
                        
                    ActivateEnemy(enemy, wave);
                }
            }
            
            if (wave is BossWave bossWave)
            {
                EnemyWaveConfig bossConfig = bossWave.GetBossConfig();

                Enemy boss = _enemyPools[bossConfig.EnemyPrefab.Type].GetEnemy();
                
                boss.transform.position = _enemySpawnPoints.GetBossSpawnPointPosition();
                    
                ActivateEnemy(boss, wave);
            }

            ActualizeMusicTheme(wave, waveNumber);
            
            _waveProgressHandler.InitializeWave(_activeEnemiesCount, waveNumber);
        }

        private void ActualizeMusicTheme(EnemyWave wave, int waveNumber)
        {
            SoundID bossTheme = _audioPlayback.AudioContainer.BossTheme;

            if (wave is BossWave || waveNumber == _waves.Length)
            {
               _audioPlayback.PlayMusic(bossTheme);
            }
            else
            {
                if (_audioPlayback.AudioContainer.CurrentPlayingMusicID == bossTheme)
                    _audioPlayback.PlayLevelThemeAfterBossTheme();
            }
        }

        private void ActivateEnemy(Enemy enemy, EnemyWave wave)
        {
            enemy.gameObject.SetActive(true);
      
            enemy.ApplyWaveMultipliers(wave.PowerMultiplier);

            _activeEnemiesCount++;
        }

        private void ShuffleSpawnPoints()
        {
            var spawnPoints = _enemySpawnPoints.GetSpawnPointsPositions().ToList();

            int spawnPointsCount = spawnPoints.Count;

            while (spawnPointsCount > 1)
            {
                int randomIndex = _random.Next(spawnPointsCount--);

                Vector3 value = spawnPoints[randomIndex];

                spawnPoints[randomIndex] = spawnPoints[spawnPointsCount];

                spawnPoints[spawnPointsCount] = value;
            }

            _shuffledSpawnPoints = spawnPoints;
        }

        private Vector3 GetSpawnPosition()
        {
            if (_shuffledSpawnPoints.Count == 0)
                throw new InvalidOperationException();

            Vector3 point = _shuffledSpawnPoints.FirstOrDefault();
            
            _shuffledSpawnPoints.Remove(point);
            
            return point;
        }

        private void OnEnemyDead(Enemy enemy)
        {
            _waveProgressHandler.OnEnemyDefeated();
            
            StartCoroutine(ReturnEnemyToPool(enemy));

            _activeEnemiesCount--;

            if (_activeEnemiesCount == HealDropEnemySerialNumber)
                DropHealWithChance(enemy.transform.position);

            if (_activeEnemiesCount == 0)
            {
                if (_currentWaveNumber < _waves.Length)
                {
                    Boost boost = Instantiate(_boostsStorage.GetRandomBoost(), enemy.transform.position, Quaternion.identity);

                    _waveProgressHandler.SubscribeOnBoostTaking(boost);
                }
                
                StartNextWave();
            }
        }

        private void DropHealWithChance(Vector3 position)
        {
            if (_random.Next(100) < _healDropPercentChance)
                _ = Instantiate(_boostsStorage.GetHealBoost(), position, Quaternion.identity);
        }

        private IEnumerator ReturnEnemyToPool(Enemy enemy)
        {
            yield return new WaitForSeconds(_enemyDyingTime);

            _enemyPools[enemy.Type].ReturnEnemy(enemy);
        }
    }
}