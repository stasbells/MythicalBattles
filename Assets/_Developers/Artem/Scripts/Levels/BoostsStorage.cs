using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MythicalBattles
{
    public class BoostsStorage : MonoBehaviour
    {
        [SerializeField] private GameObject[] _statsBoosts;
        [SerializeField] private GameObject[] _projectileBoosts;
        [SerializeField] private GameObject[] _companionBoosts;
        [SerializeField] private GameObject _healBoost;

        private List<GameObject> _usedCompanions = new List<GameObject>();
        private bool _isProjectileBoostAlreadyUsed = false;

        public GameObject GetRandomBoost()
        {
            var availableGroups = new List<IEnumerable<GameObject>>();
            
            var duplicatedStatsBoosts = _statsBoosts
                .SelectMany(boost => new[] { boost, boost })
                .ToArray();
            
            availableGroups.Add(duplicatedStatsBoosts);

            if (_isProjectileBoostAlreadyUsed == false)
            {
                var duplicatedProjectileBoosts = _projectileBoosts
                    .SelectMany(boost => new[] { boost, boost })
                    .ToArray();
                
                availableGroups.Add(duplicatedProjectileBoosts);
            }
            
            var availableCompanions = _companionBoosts
                .Where(companion => _usedCompanions.Contains(companion) == false)
                .ToArray();
            
            availableGroups.Add(availableCompanions);
            
            var candidates = new List<GameObject>();
            
            foreach (var group in availableGroups)
            {
                if (group != null && group.Any())
                    candidates.AddRange(group);
            }
            
            if (candidates.Count == 0)
                throw new InvalidOperationException();
            
            GameObject selected = candidates[Random.Range(0, candidates.Count)];
            
            if (_projectileBoosts.Contains(selected))
            {
                _isProjectileBoostAlreadyUsed = true;
            }
            else if (_companionBoosts.Contains(selected))
            {
                _usedCompanions.Add(selected);
            }

            return selected;
        }

        public GameObject GetHealBoost()
        {
            return _healBoost;
        }
    }
}