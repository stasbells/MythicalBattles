using System;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Boosts
{
    public class MaxHealthBoost : Boost
    {
        [SerializeField] private float _healthMultiplier = 0.1f;

        protected override void Apply()
        {
            base.Apply();

            if (Player.TryGetComponent(out PlayerHealth playerHealth) == false)
                throw new InvalidOperationException();

            playerHealth.IncreaseMaxHealth(_healthMultiplier);

            float healAmount = Mathf.Round(playerHealth.MaxHealth.Value);

            playerHealth.Heal(healAmount);
        }
    }
}
