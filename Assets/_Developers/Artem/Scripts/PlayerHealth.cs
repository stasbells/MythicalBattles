using Reflex.Attributes;

namespace MythicalBattles
{
    public class PlayerHealth : Health
    {
        private IPlayerStats _playerStats;
        private float _startMaxHealth;

        [Inject]
        private void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
            MaxHealth.Value = _playerStats.MaxHealth.Value;
            _startMaxHealth = MaxHealth.Value;
        }
        
        public void IncreaseMaxHealth(float healthMultiplier)
        {
             float newMaxHealth = _startMaxHealth * healthMultiplier + MaxHealth.Value;
            
            ChangeMaxHealthValue(newMaxHealth);
        }
    }
}
