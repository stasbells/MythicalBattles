using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Boosts
{
    public class HealBoost : Boost
    {
        [SerializeField] private float _healFactor = 0.3f;
        
        private PlayerHealth _playerHealth;

        protected override void OnTriggerEnterBehaviour(Collider otherCollider)
        {
            if (otherCollider.TryGetComponent(out PlayerHealth playerHealth))
            {
                _playerHealth = playerHealth;
            }
        }

        protected override void Apply()
        {
            base.Apply();
            
            float healAmount = Mathf.Round(_playerHealth.MaxHealth.Value * _healFactor);
            
            _playerHealth.Heal(healAmount);
        }
    }
}
