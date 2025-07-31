using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator))]
    public abstract class Shooter : MonoBehaviour
    {
        private const float ShootDelay = 0.1f;

        [SerializeField] private float _initRateOfFire = 1f;

        private Animator _animator;
        private Coroutine _shootCoroutine;
        private float _shootSpeedAnimationMultiplier;
        private float _rateOfFire;
        private float _remainingDelay;
        private bool _wasAttacking;

        public Animator Animator => _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(Constants.IsAttack, false);
            _rateOfFire = _initRateOfFire;

            if (this is PlayerShooter)
                ApplyShootAnimationSpeed(_rateOfFire);

            OnShooterAwake();
        }

        private void OnEnable()
        {
            _remainingDelay = ShootDelay;
            _wasAttacking = false;
        }

        private void Update()
        {
            if (_animator.GetBool(Constants.IsDead))
                return;

            bool isAttacking = !_animator.GetBool(Constants.IsMove) && _animator.GetBool(Constants.IsAttack);

            if (isAttacking)
            {
                if (!_wasAttacking)
                    StartShootCoroutine();
            }
            else
            {
                if (_shootCoroutine != null)
                {
                    StopCoroutine(_shootCoroutine);
                    _shootCoroutine = null;
                }
                else
                {
                    _remainingDelay -= Time.deltaTime;
                }
            }

            _wasAttacking = isAttacking;
        }

        protected void ChangeAttackSpeed(float attackSpeedFactor)
        {
            _rateOfFire = _initRateOfFire / attackSpeedFactor;
            ApplyShootAnimationSpeed(_rateOfFire);
        }

        protected virtual void Shoot() { }

        protected virtual void OnShooterAwake() { }

        private void ApplyShootAnimationSpeed(float rateOfFire)
        {
            _shootSpeedAnimationMultiplier = 1 / rateOfFire;
            _animator.SetFloat(Constants.ShootSpeed, _shootSpeedAnimationMultiplier);
        }

        private void StartShootCoroutine()
        {
            _shootCoroutine ??= StartCoroutine(ShootWithFrequency());
        }

        private IEnumerator ShootWithFrequency()
        {
            float delay = _remainingDelay > 0 ? _remainingDelay : ShootDelay;
            float timer = 0f;

            while (timer < delay)
            {
                if (_animator.GetBool(Constants.IsDead) || _animator.GetBool(Constants.IsMove) || !_animator.GetBool(Constants.IsAttack))
                {
                    _remainingDelay = delay - timer;

                    yield break;
                }

                timer += Time.deltaTime;

                yield return null;
            }

            while (!_animator.GetBool(Constants.IsDead))
            {
                _remainingDelay = _rateOfFire;

                Shoot();

                timer = 0f;

                while (timer < _rateOfFire)
                {
                    if (_animator.GetBool(Constants.IsDead) || _animator.GetBool(Constants.IsMove) || !_animator.GetBool(Constants.IsAttack))
                    {
                        _remainingDelay = _rateOfFire - timer;

                        yield break;
                    }

                    timer += Time.deltaTime;

                    yield return null;
                }
            }
        }
    }
}