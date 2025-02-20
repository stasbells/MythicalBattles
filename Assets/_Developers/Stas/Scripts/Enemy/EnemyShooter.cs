using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    public class EnemyShooter : MonoBehaviour
    {
        private readonly int _isAttack = Animator.StringToHash("isAttack");
        private readonly int _isDead = Animator.StringToHash("isDead");

        [SerializeField] private ObjectPool _particlePool;
        [SerializeField] private Transform _shootPoint;

        [SerializeField] private float _shootSpeed = 1f;
        [SerializeField] private float _rateOfFire = 3f;

        private Animator _animator;
        private Coroutine _shooter;
        private WaitForSeconds _sleep;
        private float _restTimer = 0f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _sleep = new WaitForSeconds(_rateOfFire);
            _animator.SetBool(_isAttack, false);
        }

        private void Update()
        {
            if (_animator.GetBool(_isDead))
                return;

            if (_animator.GetBool(_isAttack))
            {
                _restTimer += Time.deltaTime;

                if (_restTimer >= 0.6f)
                    OnShoot();
            }
            else if (_shooter != null)
            {
                StopCoroutine(_shooter);
                _shooter = null;
                _restTimer = 0f;
            }
        }

        private void OnShoot()
        {
            _shooter ??= StartCoroutine(AutoShoot());
        }

        public IEnumerator AutoShoot()
        {
            while (true)
            {
                Shoot();

                yield return _sleep;
            }
        }

        private void Shoot()
        {
            EnemyProjectile particle = (EnemyProjectile)_particlePool.GetItem();

            particle.gameObject.SetActive(true);
            particle.transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);

            Rigidbody rigidbody = particle.GetComponent<Rigidbody>();
            rigidbody.velocity = _shootPoint.forward * _shootSpeed;
        }
    }
}