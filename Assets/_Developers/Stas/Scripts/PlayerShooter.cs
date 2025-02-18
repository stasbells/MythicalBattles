using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private PlayerMovementTest _movement;

        [SerializeField] private float _shootSpeed = 1f;
        [SerializeField] private float _rateOfFire = 1f;

        private Coroutine _shooter;
        private WaitForSeconds _sleep;

        private void Start()
        {
            _sleep = new WaitForSeconds(_rateOfFire);
        }

        private void OnEnable()
        {
            _movement.Shooting += OnShoot;
        }

        private void OnDisable()
        {
            _movement.Shooting -= OnShoot;
        }

        private void OnShoot()
        {
            if (_shooter != null)
                StopCoroutine(_shooter);

            _shooter = StartCoroutine(AutoShoot());
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
            arrow.SetActive(true);

            Rigidbody rigidbody = arrow.GetComponent<Rigidbody>();
            rigidbody.velocity = _shootPoint.forward * _shootSpeed;
        }
    }
}
