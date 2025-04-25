using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public abstract class CompanionBoost : Boost
    {
        protected CompanionSpawner CompanionSpawner;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover player))
            {
                CompanionSpawner companionSpawner = other.gameObject.GetComponentInChildren<CompanionSpawner>();
            
                if(companionSpawner == null)
                    throw new InvalidOperationException();

                CompanionSpawner = companionSpawner;
                
                RememberPlayerTransform(player.transform);
            
                Apply();

                Destroy(gameObject);  //потом возможно поменять на отключение и помещение в пул
            }
        }
    }
}
