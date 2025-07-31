using MythicalBattles.Assets.Scripts.Controllers.Player;
using System;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Boosts
{
    public class AttackSpeedBoost : Boost
    {
        [SerializeField] private float _additionalAttackSpeed = 0.4f;

        protected override void Apply()
        {
            base.Apply();
            
            if(Player.TryGetComponent(out PlayerShooter shooter) == false)
                throw new InvalidOperationException();
            
            shooter.IncreaseAttackSpeed(_additionalAttackSpeed);
        }
    }
}
