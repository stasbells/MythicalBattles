using UnityEngine;

namespace MythicalBattles
{
    public class ParticleEffect : ReturnableToPoolProjectile
    {
        public ParticleSystem ParticleSystem { get; internal set; }

        protected override void OnAwake()
        {
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        private void OnParticleSystemStopped()
        {
            Pool.ReturnItem(this);
        }
    }
}