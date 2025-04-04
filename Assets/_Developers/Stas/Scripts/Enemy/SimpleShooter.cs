using UnityEngine;

namespace MythicalBattles
{
    public class SimpleShooter : Shooter
    {
        [SerializeField] private ParticleSystem _prefab;

        private ParticleSystem _particle;

        protected override void Shoot()
        {
            _particle.Play();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _particle = Instantiate(_prefab, ShootPoint.position, ShootPoint.rotation);
            _particle.transform.SetParent(ShootPoint);
            _particle.Stop();
        }
    }
}