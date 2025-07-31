using System;
using System.Collections.Generic;
using System.Linq;
using MythicalBattles.Assets.Scripts.Controllers.Boosts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MythicalBattles.Assets.Scripts.Levels
{
    public class BoostsStorage : MonoBehaviour
    {
        [SerializeField] private Boost _damageBoost;
        [SerializeField] private Boost _maxHealthBoost;
        [SerializeField] private Boost _attackSpeedBoost;
        [SerializeField] private Boost _fireBoost;
        [SerializeField] private Boost _electricityBoost;
        [SerializeField] private Boost _poisonBoost;
        [SerializeField] private Boost _fireCompanionBoost;
        [SerializeField] private Boost _electricCompanionBoost;
        [SerializeField] private Boost _poisonCompanionBoost;
        [SerializeField] private Boost _healBoost;

        private Boost[] _statsBoosts;
        private Boost[] _projectileBoosts;
        private Boost[] _companionBoosts;
        private List<Boost> _usedCompanions = new();
        private bool _isProjectileBoostAlreadyUsed = false;

        private void Awake()
        {
            _statsBoosts = new[]{_damageBoost, _maxHealthBoost, _attackSpeedBoost};
            _projectileBoosts =  new[]{_fireBoost, _electricityBoost, _poisonBoost};
            _companionBoosts = new[]{_fireCompanionBoost, _electricCompanionBoost, _poisonCompanionBoost};
        }

        public Boost GetRandomBoost()
        {
            var candidates = PrepareAvailableBoosts();
            
            Boost selectedBoost = candidates[Random.Range(0, candidates.Count)];
            
            CorrectAvailableBoosts(selectedBoost);

            return selectedBoost;
        }

        private List<Boost> PrepareAvailableBoosts()
        {
            var availableGroups = new List<IEnumerable<Boost>>();
            
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
            
            var candidates = new List<Boost>();
            
            foreach (var group in availableGroups)
            {
                if (group != null && group.Any())
                    candidates.AddRange(group);
            }
            
            if (candidates.Count == 0)
                throw new InvalidOperationException();
            
            return candidates;
        }

        private void CorrectAvailableBoosts(Boost selectedBoost)
        {
            if (_projectileBoosts.Contains(selectedBoost))
            {
                _isProjectileBoostAlreadyUsed = true;
            }
            else if (_companionBoosts.Contains(selectedBoost))
            {
                _usedCompanions.Add(selectedBoost);
            }
        }

        public Boost GetHealBoost()
        {
            return _healBoost;
        }
    }
}