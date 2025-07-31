using System.Collections;
using System.Collections.Generic;
using MythicalBattles.Assets.Scripts.Utils;
using R3;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers
{
    public class EnemyHealth : Health
    {
        [SerializeField] private float _initMaxHealthValue;
        
        private readonly Dictionary<Color, Coroutine> _damageNumbersCoroutines = new();
        
        private readonly CompositeDisposable _disposable = new();

        private void OnDisable()
        {
            _disposable.Dispose();
        }
        
        public void TakeTickDamage(float timeBetweenTicks, float tickDamage, int ticksCount, Color damageNumbersColor)
        {
            if (!_damageNumbersCoroutines.ContainsKey(damageNumbersColor))
            {
                _damageNumbersCoroutines.Add(damageNumbersColor, StartCoroutine(
                    ApplyPeriodicDamage(timeBetweenTicks, tickDamage, ticksCount, damageNumbersColor)));
            }
            else
            {
                _damageNumbersCoroutines.TryGetValue(damageNumbersColor, out Coroutine damageNumbersCoroutine);

                if (damageNumbersCoroutine != null)
                    StopCoroutine(damageNumbersCoroutine);

                damageNumbersCoroutine = StartCoroutine(ApplyPeriodicDamage
                (timeBetweenTicks, tickDamage, ticksCount, damageNumbersColor));

                _damageNumbersCoroutines[damageNumbersColor] = damageNumbersCoroutine;
            }
        }

        protected override void OnEnableBehaviour()
        {
            IsDead.Subscribe(OnDeadStateChanged).AddTo(_disposable);
        }
        
        private IEnumerator ApplyPeriodicDamage(float interval, float damage, int count, Color color)
        {
            yield return new WaitForFixedUpdate();

            for (int i = 0; i < count; i++)
            {
                if(IsDead.Value)
                    break;
                
                yield return new WaitForSeconds(interval);

                TakeDamage(damage, color);
            }
        }

        public void ApplyWaveMultiplier(float multiplier)
        {
            MaxHealth.Value = _initMaxHealthValue * multiplier;

            SetCurrentHealth();
        }

        public void Reset()
        {
            Animator.SetBool(Constants.IsDead, false);
            
            IsDead.Value = false;
     
            MaxHealth.Value = _initMaxHealthValue;

            SetCurrentHealth();
        }

        protected override void OnAwakeBehaviour()
        {
            MaxHealth.Value = _initMaxHealthValue;
        }

        private void OnDeadStateChanged(bool isDead)
        {
            if (isDead == false) 
                return;
            
            foreach (KeyValuePair<Color, Coroutine> pair in _damageNumbersCoroutines)
            {
                StopCoroutine(pair.Value);
            }
        }
    }
}