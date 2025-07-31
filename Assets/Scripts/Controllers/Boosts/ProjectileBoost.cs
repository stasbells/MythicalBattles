using MythicalBattles.Assets.Scripts.Controllers.Player;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Boosts
{
    public class ProjectileBoost : Boost
    {
        [SerializeField] private ParticleSystem _projectilePrefab;
        
        private PlayerShooter _playerShooter;

        protected override void OnTriggerEnterBehaviour(Collider otherCollider)
        {
            if (otherCollider.TryGetComponent(out PlayerShooter playerShooter))
            {
                _playerShooter = playerShooter;
            }
        }

        protected override void Apply()
        {
            base.Apply();
            
            _playerShooter.SetProjectilePrefab(_projectilePrefab);
        }
    }
}
