using System;
using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    public abstract class Shooter : MonoBehaviour
    {
        [SerializeField] protected ObjectPool ProjectilePool;
        [SerializeField] protected ObjectPool EffectPool;
        [SerializeField] protected Transform ShootPoint;
        [SerializeField] protected float ArrowVelocity = 1f;
        [SerializeField] protected float Damage;
        [SerializeField] private float _initRateOfFire = 1f;
        [SerializeField] private float _shootDelay = 0.3f;

        protected Transform _transform;
        protected Animator _animator;
        private Coroutine _shootCoroutine;
        private float _shootSpeedAnimationMultiplier;
        private float _rateOfFire;
        private float _restTimer;

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            _animator.SetBool(Constants.IsAttack, false);
            
            OnAwake();
        }

        private void OnEnable()
        {
            _restTimer = 0f;
        }

        private void Update()
        {
            if (_animator.GetBool(Constants.IsDead))
                return;

            if (!_animator.GetBool(Constants.IsMove) && _animator.GetBool(Constants.IsAttack))
            {
                _restTimer += Time.deltaTime;

                if (_restTimer >= _shootDelay)
                    OnShoot();
            }
            else if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
                _shootCoroutine = null;
                _restTimer = 0f;
            }
        }

        protected void ChangeAttackSpeed(float attackSpeedFactor)
        {
            _rateOfFire = _initRateOfFire / attackSpeedFactor;
            _shootSpeedAnimationMultiplier = 1 / _rateOfFire;
            
            _animator.SetFloat("shootSpeed", _shootSpeedAnimationMultiplier);
        }

        protected virtual void Shoot() { }

        protected virtual void OnAwake()
        {
            _rateOfFire = _initRateOfFire; 
            _shootSpeedAnimationMultiplier = 1 / _rateOfFire;;
            
            _animator.SetFloat("shootSpeed", _shootSpeedAnimationMultiplier);
        }

        private void OnShoot() => _shootCoroutine ??= StartCoroutine(AutoShoot());

        private IEnumerator AutoShoot()
        {
            while (!_animator.GetBool(Constants.IsDead))
            {
                Shoot();

                yield return new WaitForSeconds(_rateOfFire);
            }
        }
    }
}