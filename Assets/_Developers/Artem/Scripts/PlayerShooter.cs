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
            Damage = _playerStats.Damage;
        }

        private void OnEnable()
        {
            _playerStats.AttackSpeedChanged += OnAttackSpeedChanged;
            _playerStats.DamageChanged += OnDamageChanged;
        }

        private void OnDisable()
        {
            _playerStats.AttackSpeedChanged -= OnAttackSpeedChanged;
            _playerStats.DamageChanged -= OnDamageChanged;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            SetProjectileDamage(Damage);
            
            ChangeAttackSpeed(_playerStats.AttackSpeed);
        }

        private void OnAttackSpeedChanged(float attackSpeed)
        {
            ChangeAttackSpeed(attackSpeed);
        }
        
        private void OnDamageChanged(float damage)
        {
            SetProjectileDamage(damage);
        }
    }
}
