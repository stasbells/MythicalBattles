using UnityEngine;

namespace MythicalBattles
{
    public class SimpleArrow : Projectile
    {
        [field: SerializeField] public int Damage { get; private set; }

        private void OnParticleCollision(GameObject other)
        {
            if (other.layer == Constants.LayerPlayer || other.layer == Constants.LayerEnemy)
                other.GetComponent<Health>().TakeDamage(Damage);
        }
    }
}