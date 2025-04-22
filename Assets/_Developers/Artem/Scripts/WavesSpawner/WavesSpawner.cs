using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;

namespace MythicalBattles
{
    public class WavesSpawner : MonoBehaviour
    {
        private const int HealDropEnemySerialNumber = 1;

        [SerializeField] private EnemyWave[] _waves;
        [SerializeField] private EnemySpawnPoints _enemySpawnPoints;
        [SerializeField] private BoostsStorage _boostsStorage;
        [SerializeField] private float _timeBetweenWaves = 5f;
        [SerializeField] private float _enemyDyingTime = 1f;
        [SerializeField] private float _healDropPercentChance = 30f;

        private Dictionary<GameObject, Queue<GameObject>> enemyPools = new Dictionary<GameObject, Queue<GameObject>>();
        private List<Vector3> _shuffledSpawnPoints = new List<Vector3>();
        private int _currentWaveIndex = -1;
        private int _activeEnemiesCount;
        private bool _isSpawning;
        private System.Random _random = new System.Random();

        private readonly CompositeDisposable _disposable = new ();

        private void Awake()
        {
            InitializePools();
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void InitializePools()
        {
            Dictionary<GameObject, int> maxCounts = new Dictionary<GameObject, int>();

            foreach (EnemyWave wave in _waves)
            {
                foreach (EnemyWaveConfig config in wave.GetConfigs())
                {
                    if (maxCounts.ContainsKey(config.enemyPrefab))
                    {
                        if (config.count + 1 > maxCounts[config.enemyPrefab])
                        {
                            maxCounts[config.enemyPrefab] = config.count + 1;
                        }
                    }
                    else
                    {
                        maxCounts.Add(config.enemyPrefab, config.count + 1);
                    }
                }
            }
            
            foreach (var pair in maxCounts)
            {
                Queue<GameObject> pool = new Queue<GameObject>();

                for (int i = 0; i < pair.Value; i++)
                {
                    GameObject enemyGameobject = Instantiate(pair.Key);
                    
                    enemyGameobject.SetActive(false);

                    Enemy enemy = enemyGameobject.GetComponent<Enemy>();
                    
                    enemy.Initialize(pair.Key);
                    
                    enemyGameobject.GetComponent<Health>().IsDead.Subscribe(value => OnEnemyDeadStateChanged(enemyGameobject, value))
                        .AddTo(_disposable);

                    pool.Enqueue(enemyGameobject);
                }

                enemyPools.Add(pair.Key, pool);
            }
            
            StartNextWave();
        }

        private void StartNextWave()
        {
            if (_currentWaveIndex >= _waves.Length - 1)
            {
                Debug.Log("All _waves completed!");
                return;
            }

            _currentWaveIndex++;
            StartCoroutine(SpawnWaveWithDelay());
        }

        private IEnumerator SpawnWaveWithDelay()
        {
            if (_currentWaveIndex > 0)
            {
                yield return new WaitForSeconds(_timeBetweenWaves);
            }

            SpawnWave(_waves[_currentWaveIndex]);
        }

        private void SpawnWave(EnemyWave wave)
        {
            _activeEnemiesCount = 0;
            
            if (!(wave is BossWave))
                ShuffleSpawnPoints();
            
            foreach (EnemyWaveConfig config in wave.GetConfigs())
            {
                for (int i = 0; i < config.count; i++)
                {
                    GameObject enemyGameobject = GetEnemyFromPool(config.enemyPrefab);

                    if (enemyGameobject != null)
                    {
                        enemyGameobject.transform.position = GetSpawnPosition(wave, config);
                        enemyGameobject.SetActive(true);
                        enemyGameobject.TryGetComponent(out Enemy enemy);
                        enemy.ApplyWaveMultiplier(wave.PowerMultiplier);
                        
                        _activeEnemiesCount++;
                    }
                }
            }
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
            if (wave is BossWave)
                return _enemySpawnPoints.GetBossSpawnPointPosition();
            
            if (_shuffledSpawnPoints.Count == 0)
                throw new InvalidOperationException();
            
            Vector3 point = _shuffledSpawnPoints.FirstOrDefault();
            _shuffledSpawnPoints.Remove(point);
            return point;
        }

        private GameObject GetEnemyFromPool(GameObject prefab)
        {
            if (enemyPools[prefab].Count <= 0)
                throw new InvalidOperationException();
            
            return enemyPools[prefab].Dequeue();
        }

        private void OnEnemyDeadStateChanged(GameObject enemy, bool isDead)
        {
            if (isDead)
            {
                StartCoroutine(ReturnEnemyToPool(enemy));

                _activeEnemiesCount--;

                if (_activeEnemiesCount == HealDropEnemySerialNumber)
                    DropHealWithChance(enemy.transform.position);
                
                if (_activeEnemiesCount == 0)
                {
                    if (_currentWaveIndex < _waves.Length - 1)
                        Instantiate(_boostsStorage.GetRandomBoost(), enemy.transform.position, Quaternion.identity);

                    StartNextWave();
                }
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

            enemyGameObject.SetActive(false);
            
            Enemy enemy = enemyGameObject.GetComponent<Enemy>();
            
            enemy.CancelWaveMultiplier();
            
            if (enemyPools.TryGetValue(enemy.Prefab, out var pool))
            {
                pool.Enqueue(enemyGameObject);
            }
            else
            {
                Debug.LogError($"No pool found for prefab: {enemy.Prefab.name}");
            }
        }
    }
}