using UnityEngine;

namespace MythicalBattles
{
    public class ProjectileBoost : Boost
    {
        [SerializeField] private ParticleSystem _projectilePrefab;
        
        private PlayerShooter _playerShooter;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerShooter playerShooter))
            {
                _playerShooter = playerShooter;

                RememberPlayer(playerShooter.transform);
                
                Apply();

                Destroy(gameObject);  //потом возможно поменять на отключение и помещение в пул
            }
        }

        protected override void Apply()
        {
            base.Apply();
            
            _playerShooter.SetProjectilePrefab(_projectilePrefab);
        }
    }
}
