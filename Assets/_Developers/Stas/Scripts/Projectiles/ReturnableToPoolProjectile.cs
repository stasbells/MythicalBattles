using UnityEngine;

namespace MythicalBattles
{
    public abstract class ReturnableToPoolProjectile : MonoBehaviour
    {
        private protected ObjectPool _pool;
        private protected Transform _transform;

        public Transform Transform => _transform;

        public void SetPool(ObjectPool pool) => _pool = pool;
    }
}   