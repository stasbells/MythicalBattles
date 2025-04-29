using System;
using UnityEngine;

namespace MythicalBattles
{
    public class MaxHealthBoost : Boost
    {
        [SerializeField] private float _healthMultiplier = 0.3f;

        protected override void Apply()
        {
            if(Player.TryGetComponent(out PlayerHealth playerHealth) == false)
                throw new InvalidOperationException();
            
            playerHealth.IncreaseMaxHealth(_healthMultiplier);
        }
    }
}
