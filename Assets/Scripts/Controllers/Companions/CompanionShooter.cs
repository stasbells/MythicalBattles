using System.Collections;
using MythicalBattles.Assets.Scripts.Controllers.Projectiles.ObjectPool;
using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Companions
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CompanionAutoAim))]
    public class CompanionShooter : MonoBehaviour
    {
        [SerializeField] protected ProjectilesObjectPool _projectilePool;
        [SerializeField] protected Transform _shootPoint;

        [SerializeField] protected float _projectileSpeed = 0.8f;
        [SerializeField] private float _rateOfFire = 1.5f;
        [SerializeField] private float _shootDelay = 1f;

        private Transform _transform;
        private Animator _animator;
        private CompanionAutoAim _autoAim;
        private Coroutine _attackCoroutine;

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            _autoAim = GetComponent<CompanionAutoAim>();
        }

        private void OnEnable()
        {
            _autoAim.EnemyFound += OnEnemyFound;
            _autoAim.EnemyMissed += OnEnemyMissed;
        }

        private void OnDisable()
        {
            _autoAim.EnemyFound -= OnEnemyFound;
            _autoAim.EnemyMissed -= OnEnemyMissed;
        }

        private void OnEnemyFound()
        {
            _attackCoroutine = StartCoroutine(Attack());
        }

        private void OnEnemyMissed()
        {
            StopCoroutine(_attackCoroutine);
        }

        private void Shoot()
        {
            CompanionProjectile projectile = (CompanionProjectile) _projectilePool.GetItem();

            projectile.gameObject.SetActive(true);

            projectile.Transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);

            projectile.Rigidbody.velocity = _shootPoint.forward * _projectileSpeed;
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(_shootDelay);

            while (_animator.GetBool(Constants.IsAttack))
            {
                Shoot();

                yield return new WaitForSeconds(_rateOfFire);
            }
        }
    }
}