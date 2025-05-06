using UnityEngine;

namespace MythicalBattles
{
    public abstract class ReturnableToPoolProjectile : MonoBehaviour
    {
        private protected ProjectilesObjectPool _pool;
        private protected Transform _transform;

        public Transform Transform => _transform;

        public void SetPool(ProjectilesObjectPool pool) => _pool = pool;
    }
}   