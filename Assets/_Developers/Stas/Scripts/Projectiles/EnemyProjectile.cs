using UnityEngine;

namespace MythicalBattles
{
    public class EnemyProjectile : ReturnableProjectile
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == Constants.LayerPlayer || collision.gameObject.layer == Constants.LayerObstacles)
            {
                gameObject.GetComponent<ParticleSystem>().Stop();
                gameObject.transform.parent = null;

                _pool.ReturnItem(this);
            }
        }
    }
}