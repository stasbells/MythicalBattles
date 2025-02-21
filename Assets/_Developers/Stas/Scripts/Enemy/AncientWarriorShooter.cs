using UnityEngine;

namespace MythicalBattles
{
    public class AncientWarriorShooter : EnemyShooter
    {
        protected override void Shoot()
        {
            Arrow arrow = (Arrow)_projectilePool.GetItem();
            ParticleEffect particle = (ParticleEffect)_particlePool.GetItem();

            arrow.gameObject.SetActive(true);
            arrow.transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);
            arrow.SetParticle(particle);

            Rigidbody rigidbody = arrow.GetComponent<Rigidbody>();
            rigidbody.velocity = _shootPoint.forward * _shootSpeed;
        }
    }
}
