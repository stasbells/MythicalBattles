using UnityEngine;
using System.Collections;

namespace MythicalBattles
{
    public class UltimateDamager : MonoBehaviour
    {
        [SerializeField] private float _cooldown = 1f;

        private Coroutine _damager;
        private WaitForSeconds _sleep;

        [field: SerializeField] public int DamageValue { get; private set; }

        private void Awake()
        {
            _sleep = new WaitForSeconds(_cooldown);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Constants.LayerPlayer)
                OnShoot(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == Constants.LayerPlayer)
                StopCoroutine(_damager);
        }

        private void OnShoot(Collider other)
        {
            _damager = StartCoroutine(OnDamage(other.GetComponent<Health>()));
        }

        private IEnumerator OnDamage(Health playerHealth)
        {
            while (true)
            {
                playerHealth.TakeDamage(DamageValue);

                yield return _sleep;
            }
        }
    }
}