using System;
using UnityEngine;

namespace MythicalBattles
{
    public class DamageBoost : Boost
    {
        [SerializeField] private float _damageMultiplier = 0.4f;

        protected override void Apply()
        {
            if(Player.TryGetComponent(out PlayerShooter shooter) == false)
                throw new InvalidOperationException();
            
            shooter.IncreaseDamage(_damageMultiplier);
        }
    }
}
