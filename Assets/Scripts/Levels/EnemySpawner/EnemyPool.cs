using System;
using System.Collections.Generic;
using MythicalBattles.Assets.Scripts.Controllers;
using MythicalBattles.Assets.Scripts.Controllers.Enemies;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MythicalBattles.Assets.Scripts.Levels.EnemySpawner
{
    public class EnemyPool
    {
        private readonly Enemy _prefab;
        private readonly Queue<Enemy> _pool = new();
        private readonly Transform _parent;
        private readonly CompositeDisposable _disposable = new ();
        private readonly Action<Enemy> _onEnemyDead;
        private int _poolSize;
        
        public EnemyPool(Enemy prefab, int poolSize, Action<Enemy> onEnemyDead, Transform parent)
        {
            if (prefab == null)
                throw new InvalidOperationException();

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

            return false;
        }

        private void InitializePool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                Enemy enemy = InstantiateEnemy();

                _pool.Enqueue(enemy);
            }
        }

        public Enemy GetEnemy()
        {
            if (_pool.Count == 0)
                return InstantiateEnemy();

            return _pool.Dequeue();
        }

        public void ReturnEnemy(Enemy enemy)
        {
            enemy.CancelWaveMultipliers();

            enemy.gameObject.SetActive(false);

            _pool.Enqueue(enemy);
        }

        private Enemy InstantiateEnemy()
        {
            Enemy enemy = Object.Instantiate(_prefab, _parent);

            enemy.gameObject.SetActive(false);

            enemy.GetComponent<Health>().IsDead
                .Subscribe(value => OnEnemyDeadStateChanged(value, enemy))
                .AddTo(_disposable);

            return enemy;
        }

        private void OnEnemyDeadStateChanged(bool isDead, Enemy enemy)
        {
            if (isDead)
                _onEnemyDead?.Invoke(enemy);
        }
    }
}
