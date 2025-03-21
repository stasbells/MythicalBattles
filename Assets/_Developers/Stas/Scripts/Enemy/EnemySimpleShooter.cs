using UnityEngine;

namespace MythicalBattles
{
    public class EnemySimpleShooter : Shooter
    {
        [SerializeField] private ParticleSystem _prefab;

        private ParticleSystem _particle;

        protected override void Shoot()
        {
            _particle.Play();
        }

        protected override void OnAwake()
        {
            _particle = Instantiate(_prefab, _shootPoint.position, _shootPoint.rotation);
            _particle.transform.SetParent(_shootPoint);
            _particle.Stop();
        }
    }
}