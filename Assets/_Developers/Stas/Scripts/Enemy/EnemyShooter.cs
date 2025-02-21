using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    abstract public class EnemyShooter : MonoBehaviour
    {
        protected readonly int _isAttack = Animator.StringToHash("isAttack");
        protected readonly int _isDead = Animator.StringToHash("isDead");

        [SerializeField] protected ObjectPool _projectilePool;
        [SerializeField] protected ObjectPool _particlePool;
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] protected ParticleSystem _prefire;
        [SerializeField] protected ParticleSystem _afterfire;

        [SerializeField] protected float _shootSpeed = 1f;
        [SerializeField] private float _rateOfFire = 3f;
        [SerializeField] private float _shootDelay = 0.6f;

        protected Transform _transform;
        protected Animator _animator;
        private Coroutine _shooter;
        private WaitForSeconds _sleep;
        private float _restTimer = 0f;

        private void Awake()
        {
            if (_prefire != null)
            {
                _prefire.Stop();
                _afterfire.Stop();
            }

            _transform = transform;
            _animator = GetComponent<Animator>();
            _animator.SetBool(_isAttack, false);
            _sleep = new WaitForSeconds(_rateOfFire);
        }

        private void Update()
        {
            if (_animator.GetBool(_isDead))
                return;

            if (_animator.GetBool(_isAttack))
            {
                _restTimer += Time.deltaTime;

                if (_prefire != null && _prefire.isStopped)
                {
                    _prefire.Play();
                    _afterfire.Play();
                }

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

        private IEnumerator AutoShoot()
        {
            while (!_animator.GetBool(_isDead))
            {
                Shoot();

                yield return _sleep;
            }
        }

        private void OnShoot() => _shooter ??= StartCoroutine(AutoShoot());
    }
}