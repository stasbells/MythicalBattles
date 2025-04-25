using R3;
using Reflex.Attributes;

namespace MythicalBattles
{
    public class PlayerShooter : SimpleShooter
    {
        private IPlayerStats _playerStats;
        
        private readonly CompositeDisposable _disposable = new();
        
        [Inject]
        private void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
            Damage = _playerStats.Damage.Value;
        }

        private void OnEnable()
        {
            _playerStats.AttackSpeed.Subscribe(OnAttackSpeedChanged).AddTo(_disposable);
            _playerStats.Damage.Subscribe(OnDamageChanged).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            SetProjectileDamage(Damage);
            
            ChangeAttackSpeed(_playerStats.AttackSpeed.Value);
        }

        protected override void InstantiateNewProjectileParticle()
        {
            base.InstantiateNewProjectileParticle();
            
            SetProjectileDamage(_playerStats.Damage.Value);
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
