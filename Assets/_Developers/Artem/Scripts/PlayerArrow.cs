using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class PlayerArrow : SimpleProjectile
    {
        private IPlayerStats _playerStats;
        
        private void Construct()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
            
            ChangeDamage(_playerStats.Damage);
        }

        private void Awake()
        {
            Construct();
        }

        private void OnEnable()
        {
            _playerStats.DamageChanged += OnDamageChanged;
        }

        private void OnDisable()
        {
            _playerStats.DamageChanged -= OnDamageChanged;
        }

        private void OnDamageChanged(float damage)
        {
            ChangeDamage(damage);
        }
    }
}
