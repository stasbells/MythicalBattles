using R3;
using Reflex.Attributes;

namespace MythicalBattles
{
    public class PlayerShooter : SimpleShooter
    {
        private float _startDamage;
        private float _startAttackSpeed;
        private float _attackSpeed;

        [Inject]
        private void Construct(IPlayerStats playerStats)
        {
            _startDamage = playerStats.Damage.Value;
            _attackSpeed = playerStats.AttackSpeed.Value;
            Damage = _startDamage;
        }
        
        public void IncreaseDamage(float damageMultiplier)
        {
            Damage += _startDamage*damageMultiplier;
            
            SetProjectileDamage(Damage);
        }
        
        public void IncreaseAttackSpeed(float attackSpeedFactor)
        {
            _attackSpeed += attackSpeedFactor;
            
            ChangeAttackSpeed(_attackSpeed);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            SetProjectileDamage(Damage);
            
            ChangeAttackSpeed(_attackSpeed);
        }

        protected override void InstantiateNewProjectileParticle()
        {
            base.InstantiateNewProjectileParticle();
            
            SetProjectileDamage(Damage);
        }
    }
}
