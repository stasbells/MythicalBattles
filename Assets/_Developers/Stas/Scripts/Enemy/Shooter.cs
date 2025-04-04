using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    public abstract class Shooter : MonoBehaviour
    {
        [SerializeField] protected ObjectPool ProjectilePool;
        [SerializeField] protected ObjectPool EffectPool;
        [SerializeField] protected Transform ShootPoint;
        //[SerializeField] protected ParticleSystem Prefire;
        //[SerializeField] protected ParticleSystem Afterfire;

        [SerializeField] protected float ArrowVelocity = 1f;
        [SerializeField] private float _initRateOfFire = 1f;
        [SerializeField] private float _shootDelay = 0.3f;

        protected Transform _transform;
        protected Animator _animator;
        private Coroutine _shooter;
        private float _rateOfFire;
        private float _restTimer = 0f;

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            _animator.SetBool(Constants.IsAttack, false);

            OnAwake();
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
            else if (_shooter != null)
            {
                StopCoroutine(_shooter);
                _shooter = null;
                _restTimer = 0f;
            }
        }

        protected void ChangeAttackSpeed(float attackSpeedFactor)
        {
            _rateOfFire = _initRateOfFire / attackSpeedFactor;
        }

        protected virtual void Shoot() { }

        protected virtual void OnAwake()
        {
            _rateOfFire = _initRateOfFire;
        }

        private void OnShoot() => _shooter ??= StartCoroutine(AutoShoot());

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