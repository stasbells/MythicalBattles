using Ami.BroAudio;
using MythicalBattles.Assets.Scripts.Services.AudioPlayback;
using MythicalBattles.Assets.Scripts.Services.PlayerStats;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets.Scripts.Controllers
{
    public class PlayerHealth : Health
    {
        private float _startMaxHealth;
        private IAudioPlayback _audioPlayback;
        
        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            MaxHealth.Value = container.Resolve<IPlayerStats>().MaxHealth.Value;
            _startMaxHealth = MaxHealth.Value;
            _audioPlayback = container.Resolve<IAudioPlayback>();
        }
        
        protected override void OnAwakeBehaviour()
        {
            Construct();
        }

        public void IncreaseMaxHealth(float healthMultiplier)
        {
            float newMaxHealth = _startMaxHealth * healthMultiplier + MaxHealth.Value;
            
            ChangeMaxHealthValue(newMaxHealth);
        }

        public override void TakeDamage(float damage)
        {
            if (IsDead.Value)
                return;
            
            base.TakeDamage(damage);

            SoundID damageSound = _audioPlayback.AudioContainer.PlayerDamaged;
            
            _audioPlayback.PlaySound(damageSound);
        }

        protected override void Die()
        {
            base.Die();

            SoundID deathSound = _audioPlayback.AudioContainer.PlayerDeath;
            
            _audioPlayback.PlaySound(deathSound);
        }
    }
}