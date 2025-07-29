using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class PlayerShooter : SimpleShooter
    {
        private float _startDamage;
        private float _attackSpeed;
        private IPlayerStats _playerStats;

        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _startDamage = container.Resolve<IPlayerStats>().Damage.Value;
            _attackSpeed = container.Resolve<IPlayerStats>().AttackSpeed.Value;
            
        }

        public void IncreaseDamage(float damageMultiplier)
        {
            SetDamage(Damage + _startDamage * damageMultiplier);

            SetProjectileDamage(Damage);
        }

        public void IncreaseAttackSpeed(float attackSpeedFactor)
        {
            _attackSpeed += attackSpeedFactor;

            ChangeAttackSpeed(_attackSpeed);
        }

        protected override void OnSimpleShooterAwake()
        {
            Construct();

            SetDamage(_startDamage);

            SetProjectileDamage(Damage);

            ChangeAttackSpeed(_attackSpeed);
        }

        protected override void OnInstantiateNewProjectileParticle()
        {
            SetProjectileDamage(Damage);
        }
    }
}