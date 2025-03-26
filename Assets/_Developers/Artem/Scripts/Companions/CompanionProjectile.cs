using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MythicalBattles
{
    public class CompanionProjectile : Projectile
    {
        [field: SerializeField] public int Damage { get; private set; }

        private List<ParticleSystem> _particleSystems;

        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            _transform = transform;
            Rigidbody = GetComponent<Rigidbody>();
            _particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
        }

        private void OnEnable()
        {
            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.gameObject.layer == Constants.LayerEnemy)
                otherCollider.gameObject.GetComponent<Health>().TakeDamage(Damage);

            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                particleSystem.Stop();
            }

            _pool.ReturnItem(this);
        }
    }
}
