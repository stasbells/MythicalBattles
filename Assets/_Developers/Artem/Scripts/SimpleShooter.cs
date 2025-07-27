using System;
using UnityEngine;

namespace MythicalBattles
{
    public class SimpleShooter : Shooter
    {
        [SerializeField] protected Transform ShootPoint;
        [SerializeField] protected float Damage;
        [SerializeField] private ParticleSystem _projectilePrefab;
        
        private ParticleSystem _particle;
        private SimpleProjectile _projectile;
        
        protected override void OnAwake()
        {
            InstantiateNewProjectileParticle();
        }

        public void SetProjectilePrefab(ParticleSystem projectilePrefab)
        {
            _projectilePrefab = projectilePrefab;

            InstantiateNewProjectileParticle();
        }

        protected override void Shoot()
        {
            if (TryGetComponent(out Health health) == false)
                throw new InvalidOperationException();
            
            if(health.IsDead.Value == false)
                _particle.Play();
        }

        protected void SetProjectileDamage(float damage)
        {
            _projectile.SetDamage(damage);
        }

        private void InstantiateNewProjectileParticle()
        {
            if (_particle != null)
                Destroy(_particle);

            _particle = Instantiate(_projectilePrefab, ShootPoint.position, ShootPoint.rotation);

            _particle.transform.SetParent(ShootPoint);

            _particle.Stop();

            if(_particle.TryGetComponent(out SimpleProjectile projectile) == false)
                throw new InvalidOperationException();

            _projectile = projectile;

            SetProjectileDamage(Damage);
        }
    }
}
