using Ami.BroAudio;
using Reflex.Attributes;

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
            
            _audioPlayback.Play(damageSound);
        }

        protected override void Die()
        {
            base.Die();

            SoundID deathSound = _audioPlayback.AudioContainer.PlayerDeath;
            
            _audioPlayback.Play(deathSound);
        }
    }
}
