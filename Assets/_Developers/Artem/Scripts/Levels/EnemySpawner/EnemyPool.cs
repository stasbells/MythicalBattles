using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MythicalBattles
{
    public class EnemyPool
    {
        private readonly GameObject _prefab;
        private readonly Queue<GameObject> _pool = new Queue<GameObject>();
        private int _poolSize;
        private readonly Transform _parent;
        private readonly Action<GameObject> _onEnemyDead;
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        
        public EnemyPool(GameObject prefab, int poolSize, Action<GameObject> onEnemyDead, Transform parent)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab), "Prefab cannot be null.");
            
            _prefab = prefab;
            _poolSize = poolSize;
            _onEnemyDead = onEnemyDead;
            _parent = parent;
            InitializePool();
        }

        public bool TryUpdateSize(int poolSize)
        {
            if (_poolSize < poolSize)
            {
                _poolSize = poolSize;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitializePool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject enemy = Object.Instantiate(_prefab, _parent);
                
                enemy.SetActive(false);
                
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                
                enemyComponent.Initialize(_prefab);
                
                enemy.GetComponent<Health>().IsDead
                    .Subscribe(value => OnEnemyDeadStateChanged(value, enemy))
                    .AddTo(_disposable);
                
                _pool.Enqueue(enemy);
            }
        }

        public GameObject GetEnemy()
        {
            if (_pool.Count == 0)
            {
                GameObject enemy = Object.Instantiate(_prefab, _parent);
                
                enemy.SetActive(false);
                
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                
                enemyComponent.Initialize(_prefab);
                
                enemy.GetComponent<Health>().IsDead
                    .Subscribe(value => OnEnemyDeadStateChanged(value, enemy))
                    .AddTo(_disposable);
                
                return enemy;
            }
            
            return _pool.Dequeue();
        }

        public void ReturnEnemy(GameObject enemyGameObject)
        {
            Enemy enemy = enemyGameObject.GetComponent<Enemy>();
            
            enemy.CancelWaveMultipliers();
            
            enemyGameObject.SetActive(false);
            
            _pool.Enqueue(enemyGameObject);
        }

        private void OnEnemyDeadStateChanged(bool isDead, GameObject enemy)
        {
            if (isDead)
                _onEnemyDead?.Invoke(enemy);
        }
    }
}
