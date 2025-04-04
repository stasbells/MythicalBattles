using UnityEngine;

namespace MythicalBattles
{
    public class SimpleProjectile : MonoBehaviour
    {
        [field: SerializeField] public float Damage { get; private set; }

        private void OnParticleCollision(GameObject other)
        {
            if (other.layer == Constants.LayerPlayer || other.layer == Constants.LayerEnemy)
                other.GetComponent<Health>().TakeDamage(Damage);
        }

        protected void ChangeDamage(float damage)
        {
            Damage = damage;
        }
    }
}