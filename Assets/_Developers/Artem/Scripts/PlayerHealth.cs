using Reflex.Attributes;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class PlayerHealth : Health
    {
        private IPlayerStats _playerStats;
        
        [Inject]
        private void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void OnEnable()
        {
            _playerStats.MaxHealthChanged += OnMaxHealthChanged;
            
            ChangeMaxHealthValue(_playerStats.MaxHealth);
        }

        private void OnDisable()
        {
            _playerStats.MaxHealthChanged -= OnMaxHealthChanged;
        }

        private void OnMaxHealthChanged(float maxHealth)
        {
            ChangeMaxHealthValue(maxHealth);
        }
    }
}
