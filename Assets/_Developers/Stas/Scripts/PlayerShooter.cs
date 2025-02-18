using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    public class PlayerShooter : MonoBehaviour
    {
        private readonly int IsShoot = Animator.StringToHash("isShoot");
        private readonly int IsAim = Animator.StringToHash("isAim");

        [SerializeField] private ObjectPool _pool;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private PlayerMover _movement;

        [SerializeField] private float _shootSpeed = 1f;
        [SerializeField] private float _rateOfFire = 1f;

        private Animator _animator;
        private Coroutine _shooter;
        private WaitForSeconds _sleep;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _sleep = new WaitForSeconds(_rateOfFire);
            _animator.SetBool(IsShoot, false);
        }

        private void Update()
        {
            if (_animator.GetBool(IsShoot) && _animator.GetBool(IsAim))
            {
                OnShoot();
            }
            else if (_shooter != null)
            {
                StopCoroutine(_shooter);
                _shooter = null;
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
            var arrow = _pool.GetItem();

            arrow.transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);

            Rigidbody rigidbody = arrow.GetComponent<Rigidbody>();
            rigidbody.velocity = _shootPoint.forward * _shootSpeed;
        }
    }
}