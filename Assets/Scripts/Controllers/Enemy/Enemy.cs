using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(IWaveDamageMultiplier))]
    public class Enemy : MonoBehaviour
    {
        private EnemyHealth _health;
        private List<IWaveDamageMultiplier> _waveDamageMultipliers;

        private void Awake()
        {
            _health = GetComponent<EnemyHealth>();

            _waveDamageMultipliers = GetComponents<IWaveDamageMultiplier>().ToList();
        }

        public void ApplyWaveMultipliers(float multiplier)
        {
            _health.ApplyWaveMultiplier(multiplier);

            foreach (IWaveDamageMultiplier damageMultiplier in _waveDamageMultipliers)
            {
                damageMultiplier.ApplyMultiplier(multiplier);
            }
        }
        
        public void CancelWaveMultipliers()
        {
            _health.Reset();
            
            foreach (IWaveDamageMultiplier damageMultiplier in _waveDamageMultipliers)
            {
                damageMultiplier.CancelMultiplier();
            }
        }
    }
}