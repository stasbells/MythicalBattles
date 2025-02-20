using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAutoAim : MonoBehaviour
    {
        private readonly int _isAim = Animator.StringToHash("isAim");

        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _aimRadius;

        private Collider[] _hitColliders;
        private Animator _animator;
        private Transform _nearestEnemy;
        private Transform _targetEnemy;
        private Transform _transform;

        private float _rotationToTarget;

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            FindNearestEnemy();

            if (_targetEnemy != null)
                TurnToTargetEnemy();

            TryShoot();
        }

        private void FindNearestEnemy()
        {
            float closestDistance = Mathf.Infinity;
            _nearestEnemy = null;

            _hitColliders = Physics.OverlapSphere(_transform.position, _aimRadius, _enemyLayer);

            foreach (var hitCollider in _hitColliders)
            {
                float distance = Vector3.Distance(_transform.position, hitCollider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _nearestEnemy = hitCollider.transform;
                }
            }

            _targetEnemy = _nearestEnemy;
        }

        private void TurnToTargetEnemy()
        {
            Vector3 direction = (_targetEnemy.position - _transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            _rotationToTarget = _transform.rotation.y - lookRotation.y;
        }

        private void TryShoot()
        {
            _animator.SetBool(_isAim, _targetEnemy != null && Mathf.Abs(_rotationToTarget) < 0.1f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _aimRadius);
        }
    }
}