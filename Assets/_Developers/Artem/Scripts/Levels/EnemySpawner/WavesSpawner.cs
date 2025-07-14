using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ami.BroAudio;
using R3;
using Reflex.Extensions;
using Unity.VisualScripting;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    [RequireComponent(typeof(WaveProgressHandler))]
    public class WavesSpawner : MonoBehaviour
    {
        private const int HealDropEnemySerialNumber = 1;

        [SerializeField] private EnemyWave[] _waves;
        [SerializeField] private EnemySpawnPoints _enemySpawnPoints;
        [SerializeField] private BoostsStorage _boostsStorage;
        [SerializeField] private float _healDropPercentChance = 30f;

        private Dictionary<GameObject, EnemyPool> _enemyPools = new Dictionary<GameObject, EnemyPool>();
        private List<Vector3> _shuffledSpawnPoints = new List<Vector3>();
        private int _currentWaveNumber;
        private int _activeEnemiesCount;
        private int _timeBetweenWaves;
        private float _enemyDyingTime;
        private bool _isSpawning;
        private System.Random _random = new System.Random();
        private WaveProgressHandler _waveProgressHandler;
        private IAudioPlayback _audioPlayback;

        public int WavesCount => _waves.Length;
        public event Action AllWavesCompleted;

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
            if(timeBetweenWaves < 0)
                throw new InvalidOperationException();
            
            _timeBetweenWaves = timeBetweenWaves;
        }
        
        public void SetEnemiesDyingTime(float dyingTime)
        {
            if(dyingTime < 0)
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
            if (_enemyPools.ContainsKey(config.EnemyPrefab) == false)
            {
                _enemyPools.Add(config.EnemyPrefab, new EnemyPool(config.EnemyPrefab, config.Count + 1, OnEnemyDead, this.transform));
            }
            else
            {
                _ = _enemyPools[config.EnemyPrefab].TryUpdateSize(config.Count + 1);
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
            {
                yield return new WaitForSeconds(_timeBetweenWaves);
            }

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
                    GameObject enemyGameobject = _enemyPools[config.EnemyPrefab].GetEnemy();
                    
                    enemyGameobject.transform.position = GetSpawnPosition(wave, config);
                        
                    ActivateEnemy(enemyGameobject, wave);
                }
            }
            
            if (wave is BossWave bossWave)
            {
                EnemyWaveConfig bossConfig = bossWave.GetBossConfig();

                GameObject enemyGameobject = _enemyPools[bossConfig.EnemyPrefab].GetEnemy();
                
                enemyGameobject.transform.position = _enemySpawnPoints.GetBossSpawnPointPosition();
                    
                ActivateEnemy(enemyGameobject, wave);
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
                {
                    _audioPlayback.PlayLevelThemeAfterBossTheme();
                }
            }
        }

        private void ActivateEnemy(GameObject enemyGameobject, EnemyWave wave)
        {
            enemyGameobject.SetActive(true);
            enemyGameobject.TryGetComponent(out Enemy enemy);
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

        private Vector3 GetSpawnPosition(EnemyWave wave, EnemyWaveConfig config)
        {
            if (_shuffledSpawnPoints.Count == 0)
                throw new InvalidOperationException();

            Vector3 point = _shuffledSpawnPoints.FirstOrDefault();
            _shuffledSpawnPoints.Remove(point);
            return point;
        }

        private void OnEnemyDead(GameObject enemy)
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
                    GameObject boostObject = Instantiate(_boostsStorage.GetRandomBoost(), enemy.transform.position, Quaternion.identity);
                    
                    if(boostObject.TryGetComponent(out Boost boost) == false)
                        throw new InvalidOperationException();
                    
                    _waveProgressHandler.SubscribeOnBoostTaking(boost);
                }
                
                StartNextWave();
            }
        }

        private void DropHealWithChance(Vector3 position)
        {
            if (_random.Next(100) < _healDropPercentChance)
            {
                Instantiate(_boostsStorage.GetHealBoost(), position, Quaternion.identity);
            }
        }

        private IEnumerator ReturnEnemyToPool(GameObject enemyGameObject)
        {
            yield return new WaitForSeconds(_enemyDyingTime);

            Enemy enemy = enemyGameObject.GetComponent<Enemy>();
            
            _enemyPools[enemy.Prefab].ReturnEnemy(enemyGameObject);
        }
    }
}