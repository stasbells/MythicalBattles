using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent (typeof(Animator))]
    public class PlayerAutoAim : MonoBehaviour
    {
        private readonly int _isShootHash = Animator.StringToHash("Shoot");

        [SerializeField] private float _aimRadius = 10f;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _rotationSpeed = 5f;

        private Animator _animator;
        private Transform _targetEnemy;
        private Collider[] _hitColliders;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            FindNearestEnemy();

            if (_targetEnemy != null)
                TurnToTargetEnemy();

            Shoot();
        }

        void FindNearestEnemy()
        {
            _hitColliders = Physics.OverlapSphere(transform.position, _aimRadius, _enemyLayer);
            float closestDistance = Mathf.Infinity;
            Transform nearestEnemy = null;

            foreach (var hitCollider in _hitColliders)
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = hitCollider.transform;
                }
            }

            _targetEnemy = nearestEnemy;
        }

        private void TurnToTargetEnemy()
        {
            Vector3 direction = (_targetEnemy.position - transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        private void Shoot()
        {
            _animator.SetBool(_isShootHash, _targetEnemy != null);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _aimRadius);
        }
    }
}