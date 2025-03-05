using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    public class PlayerShooter : MonoBehaviour
    {

        [SerializeField] private ObjectPool _arrowsPool;
        [SerializeField] private ObjectPool _particlePool;
        [SerializeField] private Transform _shootPoint;

        [SerializeField] private float _shootSpeed = 1f;
        [SerializeField] private float _rateOfFire = 1f;

        private Animator _animator;
        private Coroutine _shooter;
        private WaitForSeconds _sleep;
        private float _restTimer = 0f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _sleep = new WaitForSeconds(_rateOfFire);
            _animator.SetBool(Constants.IsAim, false);
        }

        private void Update()
        {
            if (!_animator.GetBool(Constants.IsMove) && _animator.GetBool(Constants.IsAim))
            {
                _restTimer += Time.deltaTime;

                if (_restTimer >= 0.3f)
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
            Arrow arrow = (Arrow)_arrowsPool.GetItem();
            ParticleEffect particle = (ParticleEffect)_particlePool.GetItem();

            arrow.gameObject.SetActive(true);
            arrow.transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);
            arrow.SetParticle(particle);

            Rigidbody rigidbody = arrow.GetComponent<Rigidbody>();
            rigidbody.velocity = _shootPoint.forward * _shootSpeed;
        }
    }
}