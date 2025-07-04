using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MythicalBattles
{
    public class BoostsStorage : MonoBehaviour
    {
        [SerializeField] private GameObject _damageBoost;
        [SerializeField] private GameObject _maxHealthBoost;
        [SerializeField] private GameObject _attackSpeedBoost;
        [SerializeField] private GameObject _fireBoost;
        [SerializeField] private GameObject _electricityBoost;
        [SerializeField] private GameObject _poisonBoost;
        [SerializeField] private GameObject _fireCompanionBoost;
        [SerializeField] private GameObject _electricCompanionBoost;
        [SerializeField] private GameObject _poisonCompanionBoost;
        [SerializeField] private GameObject _healBoost;

        private GameObject[] _statsBoosts;
        private GameObject[] _projectileBoosts;
        private GameObject[] _companionBoosts;
        private List<GameObject> _usedCompanions = new List<GameObject>();
        private bool _isProjectileBoostAlreadyUsed = false;

        private void Awake()
        {
            _statsBoosts = new[]{_damageBoost, _maxHealthBoost, _attackSpeedBoost};
            _projectileBoosts =  new[]{_fireBoost, _electricityBoost, _poisonBoost};
            _companionBoosts = new[]{_fireCompanionBoost, _electricCompanionBoost, _poisonCompanionBoost};
        }

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