using UnityEngine;

namespace MythicalBattles
{
    public class EnemyProjectile : Projectile
    {
        private readonly int _playerLayer = 7;
        private readonly int _obstaclesLayer = 6;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == _playerLayer || collision.gameObject.layer == _obstaclesLayer)
            {
                gameObject.GetComponent<ParticleSystem>().Stop();
                gameObject.transform.parent = null;

                _pool.ReturnItem(this);
            }
        }
    }
}