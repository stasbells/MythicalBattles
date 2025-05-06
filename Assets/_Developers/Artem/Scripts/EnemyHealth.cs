using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace MythicalBattles
{
    public class EnemyHealth : Health
    {
        [SerializeField] private float _initMaxHealthValue;
        
        private readonly Dictionary<Color, Coroutine> _colorsCoroutines = new Dictionary<Color, Coroutine>();
        
        private readonly CompositeDisposable _disposable = new();

        private void OnDisable()
        {
            _disposable.Dispose();
        }
        
        public void TakeTickDamage(float timeBetweenTicks, float tickDamage, int ticksCount, Color damageNumbersColor)
        {
            if (!_colorsCoroutines.ContainsKey(damageNumbersColor))
            {
                _colorsCoroutines.Add(damageNumbersColor, StartCoroutine(
                    ApplyPeriodicDamage(timeBetweenTicks, tickDamage, ticksCount, damageNumbersColor)));
            }
            else
            {
                _colorsCoroutines.TryGetValue(damageNumbersColor, out Coroutine coroutine);

                if (coroutine != null)
                    StopCoroutine(coroutine);

                coroutine = StartCoroutine(ApplyPeriodicDamage(timeBetweenTicks, tickDamage, ticksCount,
                    damageNumbersColor));

                _colorsCoroutines[damageNumbersColor] = coroutine;
            }
        }

        protected override void OnEnableBehaviour()
        {
            base.OnEnableBehaviour();

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

            CurrentHealth = MaxHealth.Value;
        }

        public void Reset()
        {
            Animator.SetBool(Constants.IsDead, false);
            IsDead.Value = false;
     
            MaxHealth.Value = _initMaxHealthValue;

            CurrentHealth = MaxHealth.Value;
        }

        protected override void OnAwakeBehaviour()
        {
            MaxHealth.Value = _initMaxHealthValue;
        }

        private void OnDeadStateChanged(bool isDead)
        {
            if (isDead)
            {
                foreach (KeyValuePair<Color, Coroutine> pair in _colorsCoroutines)
                {
                    StopCoroutine(pair.Value);
                }
            }
        }
    }
}