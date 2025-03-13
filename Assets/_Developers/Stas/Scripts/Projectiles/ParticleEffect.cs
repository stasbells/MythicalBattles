using UnityEngine;

namespace MythicalBattles
{
    public class ParticleEffect : Projectile
    {
        private Transform _transform;

        public Transform Transform => _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnParticleSystemStopped()
        {
            _pool.ReturnItem(this);
        }
    }
}