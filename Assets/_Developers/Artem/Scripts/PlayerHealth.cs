using R3;
using Reflex.Attributes;

namespace MythicalBattles
{
    public class PlayerHealth : Health
    {
        private IPlayerStats _playerStats;
        
        private readonly CompositeDisposable _disposable = new();
        
        [Inject]
        private void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        protected override void OnEnableBehaviour()
        {
            _playerStats.MaxHealth.Subscribe(OnMaxHealthChanged).AddTo(_disposable);
            
            ChangeMaxHealthValue(_playerStats.MaxHealth.Value);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void OnMaxHealthChanged(float maxHealth)
        {
            ChangeMaxHealthValue(maxHealth);
        }
    }
}
