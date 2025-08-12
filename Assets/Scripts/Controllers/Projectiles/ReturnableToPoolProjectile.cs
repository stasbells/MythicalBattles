using MythicalBattles.Assets.Scripts.Controllers.Projectiles.ObjectPool;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Projectiles
{
    public abstract class ReturnableToPoolProjectile : MonoBehaviour
    {
        private Transform _transform;
        private ProjectilesObjectPool _pool;

        public ProjectilesObjectPool Pool => _pool;
        public Transform Transform => _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();

            OnAwake();
        }

        public void SetPool(ProjectilesObjectPool pool) => _pool = pool;

        protected virtual void OnAwake() { }
    }
}