using System;
using UnityEngine;

namespace MythicalBattles
{
    public class SimpleShooter : Shooter
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ParticleSystem _projectilePrefab;
        [SerializeField] private float _damage;

        private ParticleSystem _particle;
        private SimpleProjectile _projectile;

        protected float Damage => _damage;

        public void SetProjectilePrefab(ParticleSystem projectilePrefab)
        {
            _projectilePrefab = projectilePrefab;

            InstantiateNewProjectileParticle();
        }

        protected void SetDamage(float damage)
        {
            _damage = damage;
        }

        protected virtual void OnSimpleShooterAwake() { }

        protected virtual void OnInstantiateNewProjectileParticle() { }

        protected override void OnShooterAwake()
        {
            InstantiateNewProjectileParticle();

            OnSimpleShooterAwake();
        }

        protected override void Shoot()
        {
            if (TryGetComponent(out Health health) == false)
                throw new InvalidOperationException();

            if (health.IsDead.Value == false)
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

            _particle = Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);

            _particle.transform.SetParent(_shootPoint);
            _particle.Stop();

            if(_particle.TryGetComponent(out SimpleProjectile projectile) == false)
                throw new InvalidOperationException();

            _projectile = projectile;

            SetProjectileDamage(_damage);

            OnInstantiateNewProjectileParticle();
        }
    }
}