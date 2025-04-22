using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class HealBoost : Boost
    {
        [SerializeField] private int _healAmount;

        private PlayerHealth _playerHealth;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                _playerHealth = playerHealth;
            
                Apply();

                Destroy(gameObject);  //потом возможно поменять на отключение и помещение в пул
            }
        }
        
        protected override void Apply()
        {
            _playerHealth.Heal(_healAmount);
        }
    }
}
