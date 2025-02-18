using UnityEngine;

namespace MythicalBattles
{
    public class Projectile : MonoBehaviour
    {
        private readonly int EnemyLayer = 3;
        private readonly int ObstaclesLayer = 6;

        private ObjectPool _pool;

        public void SetPool(ObjectPool pool) { _pool = pool; }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == EnemyLayer || collision.gameObject.layer == ObstaclesLayer)
                _pool.ReturnItem(this);
        }
    }
}