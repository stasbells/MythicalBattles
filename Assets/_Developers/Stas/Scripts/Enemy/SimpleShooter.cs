using UnityEngine;

namespace MythicalBattles
{
    public class SimpleShooter : Shooter, IDamageDealComponent
    {
        [SerializeField] private ParticleSystem _prefab;

        private ParticleSystem _particle;
        private SimpleProjectile _projectile;

        public void ApplyWaveDamageMultiplier(float multiplier)
        {
            SetProjectileDamage(Damage * multiplier);
        }

        public void CancelWaveDamageMultiplier()
        {
            SetProjectileDamage(Damage);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _particle = Instantiate(_prefab, ShootPoint.position, ShootPoint.rotation);
            
            _particle.transform.SetParent(ShootPoint);
            
            _particle.Stop();
            
            _particle.TryGetComponent(out SimpleProjectile projectile);

            _projectile = projectile;
            
            SetProjectileDamage(Damage);
            
        }

        protected override void Shoot()
        {
            _particle.Play();
        }
        
        protected void SetProjectileDamage(float damage)
        {
            _projectile.SetDamage(damage);
        }
    }
}