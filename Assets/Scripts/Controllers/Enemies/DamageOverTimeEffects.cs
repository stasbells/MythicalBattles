using System.Collections;
using R3;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    public class DamageOverTimeEffects : MonoBehaviour
    {
        [SerializeField] private GameObject _fireParticle;
        [SerializeField] private GameObject _poisonParticle;

        private EnemyHealth _health;
        private Coroutine _fireEffectCoroutine;
        private Coroutine _poisonEffectCoroutine;
        
        private readonly CompositeDisposable _disposable = new();

        private void Awake()
        {
            _health = GetComponent<EnemyHealth>(); 
        }

        private void OnEnable()
        {
            _fireParticle.SetActive(false);
            _poisonParticle.SetActive(false);

            _health.IsDead.Subscribe(OnDeadStateChanged).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        public void PlayFireEffect(float timeBetweenTicks, int ticksCount)
        {
            PlayEffect(ref _fireEffectCoroutine, _fireParticle, timeBetweenTicks, ticksCount);
        }
        
        public void PlayPoisonEffect(float timeBetweenTicks, int ticksCount)
        {
            PlayEffect(ref _poisonEffectCoroutine, _poisonParticle, timeBetweenTicks, ticksCount);
        }

        private void PlayEffect(ref Coroutine effectCoroutine, GameObject particle, float timeBetweenTicks, int ticksCount)
        {
            if (effectCoroutine != null)
            {
                particle.SetActive(false);
                StopCoroutine(effectCoroutine);
            }

            effectCoroutine = StartCoroutine(StartEffect(particle, timeBetweenTicks, ticksCount));
        }

        private IEnumerator StartEffect(GameObject effect, float timeBetweenTicks, int ticksCount)
        {
            yield return new WaitForFixedUpdate();

            effect.SetActive(true);

            for (int i = 0; i < ticksCount; i++)
            {
                if (_health.IsDead.Value)
                {
                    effect.SetActive(false);
                    break;
                }
                
                yield return new WaitForSeconds(timeBetweenTicks);
            }
            
            effect.SetActive(false);
        }

        private void OnDeadStateChanged(bool isDead)
        {
            if (isDead)
            {
                if (_fireEffectCoroutine != null)
                    StopCoroutine(_fireEffectCoroutine);

                if (_poisonEffectCoroutine != null)
                    StopCoroutine(_poisonEffectCoroutine);
            }
            
            _disposable.Dispose();
        }
    }
}
