using MythicalBattles.Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies
{
    public class UltimateDamager : MonoBehaviour, IWaveDamageMultiplier
    {
        [SerializeField] private float _damagePeriod = 0.4f;
        [SerializeField] private float _initDamageValue;

        private Coroutine _damageCoroutine;
        private WaitForSeconds _delay;
        private float _damage;
        private bool _isPlayerGetDamage;

        private void Awake()
        {
            _delay = new WaitForSeconds(_damagePeriod);
            _damage = _initDamageValue;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Constants.LayerPlayer && _isPlayerGetDamage == false)
            {
                _isPlayerGetDamage = true;
                Damage(other.GetComponent<Health>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == Constants.LayerPlayer)
                _isPlayerGetDamage = false;
        }

        public void ApplyMultiplier(float multiplier)
        {
            _damage = _initDamageValue * multiplier;
        }

        public void CancelMultiplier()
        {
            _damage = _initDamageValue;
        }

        private void Damage(Health playerHealth)
        {
            _damageCoroutine ??= StartCoroutine(DamageWithDelay(playerHealth));
        }

        private IEnumerator DamageWithDelay(Health playerHealth)
        {
            while (_isPlayerGetDamage)
            {
                playerHealth.TakeDamage(_damage);

                yield return _delay;
            }

            _damageCoroutine = null;
        }
    }
}