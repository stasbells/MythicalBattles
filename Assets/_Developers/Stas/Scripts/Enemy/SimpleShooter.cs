using UnityEngine;

namespace MythicalBattles
{
    public class SimpleShooter : Shooter, IDamageDealComponent
    {
        [SerializeField] private ParticleSystem _projectilePrefab;

        private ParticleSystem _particle;
        private SimpleProjectile _projectile;

        protected override void OnAwake()
        {
            base.OnAwake();

            InstantiateNewProjectileParticle();
        }
        
        public void ApplyWaveDamageMultiplier(float multiplier)
        {
            SetProjectileDamage(Damage * multiplier);
        }

        public void CancelWaveDamageMultiplier()
        {
            SetProjectileDamage(Damage);
        }

        public void SetProjectilePrefab(ParticleSystem projectilePrefab)
        {
            _projectilePrefab = projectilePrefab;

            InstantiateNewProjectileParticle();
        }

        protected override void Shoot()
        {
            _particle.Play();
        }

        protected void SetProjectileDamage(float damage)
        {
            _projectile.SetDamage(damage);
        }

        protected virtual void InstantiateNewProjectileParticle()
        {
            if (_particle != null)
                Destroy(_particle);

            _particle = Instantiate(_projectilePrefab, ShootPoint.position, ShootPoint.rotation);

            _particle.transform.SetParent(ShootPoint);

            _particle.Stop();

            _particle.TryGetComponent(out SimpleProjectile projectile);

            _projectile = projectile;

            SetProjectileDamage(Damage);
        }
    }
}