using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAutoAim : MonoBehaviour
    {
        private readonly int IsAim = Animator.StringToHash("isAim");

        [SerializeField] private float _aimRadius;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _rotationSpeed = 5f;

        private float _rotationToTarget;

        private Transform _transform;
        private Transform _nearestEnemy;
        private Transform _targetEnemy;
        private Animator _animator;
        private Collider[] _hitColliders;

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
            _animator.SetBool(IsAim, _targetEnemy != null && Mathf.Abs(_rotationToTarget) < 0.1f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_transform.position, _aimRadius);
        }
    }
}