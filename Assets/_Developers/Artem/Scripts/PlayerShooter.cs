using MythicalBattles.Services.PlayerStats;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class PlayerShooter : SimpleShooter
    {
        private float _startDamage;
        private float _startAttackSpeed;
        private float _attackSpeed;
        private IPlayerStats _playerStats;
        
        private void Construct()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
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
            Construct();
            
            base.OnAwake();
            
            _startDamage = _playerStats.Damage.Value;
            _attackSpeed = _playerStats.AttackSpeed.Value;
            Damage = _startDamage;

            SetProjectileDamage(Damage);
            
            ChangeAttackSpeed(_attackSpeed);
        }
    }
}
