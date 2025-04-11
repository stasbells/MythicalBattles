using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.Profiling;
using Random = UnityEngine.Random;

namespace MythicalBattles
{
    public class WavesSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyWave[] waves;
        [SerializeField] private EnemySpawnPoints enemySpawnPoints;
        [SerializeField] private float timeBetweenWaves = 5f;
        [SerializeField] private float enemyDyingTime = 1f;

        private Dictionary<GameObject, Queue<GameObject>> enemyPools = new Dictionary<GameObject, Queue<GameObject>>();
        private List<Vector3> _currentAvailablePoints = new List<Vector3>();
        private int currentWaveIndex = -1;
        private int activeEnemiesCount;
        private bool isSpawning;

        private readonly CompositeDisposable _disposable = new ();

        private void Start()
        {
            StartCoroutine(InitializePools());
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private IEnumerator InitializePools()
        {
            // Находим максимальное количество для каждого типа врага во всех волнах
            Dictionary<GameObject, int> maxCounts = new Dictionary<GameObject, int>();

            foreach (EnemyWave wave in waves)
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

            // Создаем пулы
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

                    yield return null;
                }

                enemyPools.Add(pair.Key, pool);
            }
            
            StartNextWave();
        }

        private void StartNextWave()
        {
            if (currentWaveIndex >= waves.Length - 1)
            {
                Debug.Log("All waves completed!");
                return;
            }

            currentWaveIndex++;
            StartCoroutine(SpawnWaveWithDelay());
        }

        private IEnumerator SpawnWaveWithDelay()
        {
            if (currentWaveIndex > 0)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }

            SpawnWave(waves[currentWaveIndex]);
        }

        private void SpawnWave(EnemyWave wave)
        {
            activeEnemiesCount = 0;
            
            _currentAvailablePoints = new List<Vector3>(enemySpawnPoints.GetSpawnPointsPositions());

            Profiler.BeginSample("WaveSpawn");
            
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
                        
                        activeEnemiesCount++;
                    }
                }
            }
            
            Profiler.EndSample();
        }

        private Vector3 GetSpawnPosition(EnemyWave wave, EnemyWaveConfig config)
        {
            //СДЕЛАТЬ ПРОВЕРКУ ЧТО ЕСЛИ БОСС ТО return enemySpawnPoints.GetBossSpawnPointPosition();
            
            //int randomIndex = Random.Range(0, _currentAvailablePoints.Count);

            int randomIndex = 4;
            
            Vector3 selectedPoint = _currentAvailablePoints[randomIndex];
            
            //_currentAvailablePoints.RemoveAt(randomIndex);
    
            return selectedPoint;
        }

        private GameObject GetEnemyFromPool(GameObject prefab)
        {
            if (enemyPools[prefab].Count <= 0)
                Debug.LogError($"Пул исчерпан: {prefab.name}");
            
            return enemyPools[prefab].Dequeue();
        }

        private void OnEnemyDeadStateChanged(GameObject enemy, bool isDead)
        {
            if (isDead)
            {
                StartCoroutine(ReturnEnemyToPool(enemy));

                activeEnemiesCount--;

                if (activeEnemiesCount <= 0)
                {
                    StartNextWave();
                }
            }
        }

        private IEnumerator ReturnEnemyToPool(GameObject enemyGameObject)
        {
            yield return new WaitForSeconds(enemyDyingTime);

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