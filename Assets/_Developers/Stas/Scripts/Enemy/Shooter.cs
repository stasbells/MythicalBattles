using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    public abstract class Shooter : MonoBehaviour
    {
        [SerializeField] protected ObjectPool _projectilePool;
        [SerializeField] protected ObjectPool _effectPool;
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] protected ParticleSystem _prefire;
        [SerializeField] protected ParticleSystem _afterfire;

        [SerializeField] protected float _arrowVelcity = 1f;
        [SerializeField] private float _rateOfFire = 1f;
        [SerializeField] private float _shootDelay = 0.3f;

        protected Transform _transform;
        protected Animator _animator;
        private Coroutine _shooter;
        private WaitForSeconds _sleep;
        private float _restTimer = 0f;

        private void Awake()
        {
            if (_prefire != null)
                _prefire.Stop();

            if (_afterfire != null)
                _afterfire.Stop();

            _transform = transform;
            _animator = GetComponent<Animator>();
            _animator.SetBool(Constants.IsAttack, false);
            _sleep = new WaitForSeconds(_rateOfFire);

            OnAwake();
        }

        private void Update()
        {
            if (_animator.GetBool(Constants.IsDead))
                return;

            if (!_animator.GetBool(Constants.IsMove) && _animator.GetBool(Constants.IsAttack))
            {
                _restTimer += Time.deltaTime;

                if (_prefire != null && _prefire.isStopped)
                    _prefire.Play();

                if (_afterfire != null && _afterfire.isStopped)
                    _afterfire.Play();

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

        protected virtual void Shoot() { }

        protected virtual void OnAwake() { }

        private void OnShoot() => _shooter ??= StartCoroutine(AutoShoot());

        private IEnumerator AutoShoot()
        {
            while (!_animator.GetBool(Constants.IsDead))
            {
                Shoot();

                yield return _sleep;
            }
        }
    }
}