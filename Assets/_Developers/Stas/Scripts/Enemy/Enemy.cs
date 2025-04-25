using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(EnemyHealth))]
    public class Enemy : MonoBehaviour
    {
        private EnemyHealth _health;
        private IDamageDealComponent _damageDealComponent;

        public GameObject Prefab { get; private set; }

        public void Initialize(GameObject prefab)
        {
            Prefab = prefab;
        }
        private void Awake()
        {
            _health = GetComponent<EnemyHealth>();

            if (TryGetComponent(out IDamageDealComponent damageDealComponent))
            {
                _damageDealComponent = damageDealComponent;
            }
        }

        public void ApplyWaveMultiplier(float multiplier)
        {
            _health.ApplyWaveMultiplier(multiplier);
            _damageDealComponent.ApplyWaveDamageMultiplier(multiplier);
        }
        
        public void CancelWaveMultiplier()
        {
            _health.Reset();
            _damageDealComponent.CancelWaveDamageMultiplier();
        }
    }
}