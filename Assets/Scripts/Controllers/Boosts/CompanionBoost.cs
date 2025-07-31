using System;
using MythicalBattles.Assets.Scripts.Controllers.Companions;
using MythicalBattles.Assets.Scripts.Controllers.Player;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Boosts
{
    public abstract class CompanionBoost : Boost
    {
        protected CompanionSpawner CompanionSpawner;

        protected override void OnTriggerEnterBehaviour(Collider otherCollider)
        {
            if (otherCollider.TryGetComponent(out PlayerMover player))
            {
                CompanionSpawner companionSpawner = otherCollider.gameObject.GetComponentInChildren<CompanionSpawner>();
            
                if(companionSpawner == null)
                    throw new InvalidOperationException();

                CompanionSpawner = companionSpawner;
            }
        }
    }
}
