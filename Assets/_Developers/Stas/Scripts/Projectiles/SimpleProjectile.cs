using UnityEngine;

namespace MythicalBattles
{
    public class SimpleProjectile : MonoBehaviour
    {
        private float _damage;

        private void OnParticleCollision(GameObject other)
        {
            if (other.layer == Constants.LayerPlayer || other.layer == Constants.LayerEnemy)
                other.GetComponent<Health>().TakeDamage(_damage);
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public float GetDamage()
        {
            return _damage;
        }
    }
}