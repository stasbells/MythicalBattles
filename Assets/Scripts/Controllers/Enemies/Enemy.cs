using System.Collections.Generic;
using System.Linq;
using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(IWaveDamageMultiplier))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyTypes _type;
        
        private EnemyHealth _health;
        private List<IWaveDamageMultiplier> _waveDamageMultipliers;

        public EnemyTypes Type => _type;

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