using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Projectiles
{
    public class DemonSpellEffect : ReturnableToPoolProjectile
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