using UnityEngine;

namespace MythicalBattles
{
    public class ParticleEffect : ReturnableProjectile
    {
        public ParticleSystem ParticleSystem { get; internal set; }

        private void Awake()
        {
            _transform = transform;
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        private void OnParticleSystemStopped()
        {
            _pool.ReturnItem(this);
        }
    }
}