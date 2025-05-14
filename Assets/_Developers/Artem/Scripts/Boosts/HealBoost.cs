using System.Collections;
using System.Collections.Generic;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class HealBoost : Boost
    {
        [SerializeField] private float _healFactor = 0.4f;
        
        private PlayerHealth _playerHealth;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                _playerHealth = playerHealth;
                
                RememberPlayer(playerHealth.transform);
            
                Apply();

                Destroy(gameObject);  //потом возможно поменять на отключение и помещение в пул
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
