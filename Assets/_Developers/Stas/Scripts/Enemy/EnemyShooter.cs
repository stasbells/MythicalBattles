using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    abstract public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] protected ObjectPool _projectilePool;
        [SerializeField] protected ObjectPool _particlePool;
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] protected ParticleSystem _prefire;
        [SerializeField] protected ParticleSystem _afterfire;

        [SerializeField] protected float _shootSpeed = 1f;
        [SerializeField] private float _rateOfFire;
        [SerializeField] private float _shootDelay = 2f;

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
        }

        private void Start()
        {
            //CachedComponent();
        }

        private void Update()
        {
            if (_animator.GetBool(Constants.IsDead))
                return;

            if (_animator.GetBool(Constants.IsAttack))
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

        public void CachedComponent()
        {
            foreach (var item in _projectilePool.Items)
            {
                var arrow = item as Arrow;

                if (arrow != null)
                    arrow.Rigidbody = arrow.GetComponent<Rigidbody>();
            }
        }

        protected virtual void Shoot() { }

        private IEnumerator AutoShoot()
        {
            while (!_animator.GetBool(Constants.IsDead))
            {
                Shoot();

                yield return _sleep;
            }
        }

        private void OnShoot() => _shooter ??= StartCoroutine(AutoShoot());
    }
}