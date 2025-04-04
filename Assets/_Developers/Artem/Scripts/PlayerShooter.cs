using Reflex.Attributes;

namespace MythicalBattles
{
    public class PlayerShooter : SimpleShooter
    {
        private IPlayerStats _playerStats;
        
        [Inject]
        private void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void OnEnable()
        {
            _playerStats.AttackSpeedChanged += OnAttackSpeedChanged;
        }

        private void OnDisable()
        {
            _playerStats.AttackSpeedChanged -= OnAttackSpeedChanged;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            ChangeAttackSpeed(_playerStats.AttackSpeed);
        }

        private void OnAttackSpeedChanged(float attackSpeed)
        {
            ChangeAttackSpeed(attackSpeed);
        }
    }
}
