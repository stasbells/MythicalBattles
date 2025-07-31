using UnityEngine;

namespace MythicalBattles
{
    public class ParticleEffect : ReturnableToPoolProjectile
    {
        public ParticleSystem ParticleSystem { get; internal set; }

        private void OnParticleSystemStopped()
        {
            Pool.ReturnItem(this);
        }

        protected override void OnAwake()
        {
            ParticleSystem = GetComponent<ParticleSystem>();
        }
    }
}