using Ami.BroAudio;
using Reflex.Attributes;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class PlayerHealth : Health
    {
        private float _startMaxHealth;
        private IAudioPlayback _audioPlayback;

        [Inject]
        private void Construct(IPlayerStats playerStats, IAudioPlayback audioPlayback)
        {
            MaxHealth.Value = playerStats.MaxHealth.Value;
            _startMaxHealth = MaxHealth.Value;
            _audioPlayback = audioPlayback;
        }

        public void IncreaseMaxHealth(float healthMultiplier)
        {
             float newMaxHealth = _startMaxHealth * healthMultiplier + MaxHealth.Value;
            
            ChangeMaxHealthValue(newMaxHealth);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            SoundID damageSound = _audioPlayback.AudioContainer.PlayerDamaged;
            
            _audioPlayback.PlaySound(damageSound);
        }

        protected override void OnAwakeBehaviour()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            MaxHealth.Value = container.Resolve<IPlayerStats>().MaxHealth.Value;
            _startMaxHealth = MaxHealth.Value;
            _audioPlayback = container.Resolve<IAudioPlayback>();
        }

        protected override void Die()
        {
            base.Die();

            SoundID deathSound = _audioPlayback.AudioContainer.PlayerDeath;
            
            _audioPlayback.PlaySound(deathSound);
        }
    }
}