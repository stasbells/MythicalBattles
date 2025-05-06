using System;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class CompanionSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _fireCompanionPrefab;
        [SerializeField] private GameObject _poisonCompanionPrefab;
        [SerializeField] private GameObject _electricCompanionPrefab;

        [SerializeField] private List<CompanionSpot> _companionSpots;

        public void SpawnFireCompanion()
        {
            SpawnCompanion(_fireCompanionPrefab);
        }

        public void SpawnPoisonCompanion()
        {
            SpawnCompanion(_poisonCompanionPrefab);
        }
        
        public void SpawnElectricCompanion()
        {
            SpawnCompanion(_electricCompanionPrefab);
        }

        private void SpawnCompanion(GameObject companionPrefab)
        {
            foreach (var spot in _companionSpots)
            {
                if (spot.IsFilled == false)
                {
                    var newCompanionObject = Instantiate(companionPrefab, spot.transform.position, Quaternion.identity);

                    CompanionMover newCompanion = newCompanionObject.GetComponent<CompanionMover>();
                    
                    if(newCompanion == null)
                        throw new InvalidOperationException();
                    
                    spot.Fill(newCompanion);
                    
                    newCompanion.Move(spot.transform);
                    
                    return;
                }
            }
        }

        private void OnDisable()
        {
            foreach (var spot in _companionSpots)
                spot.Release();
        }
    }
}
