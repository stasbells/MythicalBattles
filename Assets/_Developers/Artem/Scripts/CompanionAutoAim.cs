using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Companion))]
    [RequireComponent(typeof(Animator))]
    public class CompanionAutoAim : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _rotationSpeed = 500f;
        [SerializeField] private float _aimRadius = 15f;

        private Companion _companion;
        private Collider[] _hitColliders;
        private Animator _animator;
        private Transform _nearestEnemy;
        private Transform _targetEnemy;
        private Transform _transform;

        private float _rotationToTarget;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _companion = GetComponent<Companion>();
            _transform = transform;
        }

        private void Update()
        {
            FindNearestEnemy();

            if (_targetEnemy)
                TurnToTargetEnemy();
            else
                TurnToSpotRotation();

            TryShoot();
        }

        private void FindNearestEnemy()
        {
            float closestDistance = Mathf.Infinity;
            _nearestEnemy = null;

            _hitColliders = new Collider[10];
            int hitCount = Physics.OverlapSphereNonAlloc(_transform.position, _aimRadius, _hitColliders, _enemyLayer);

            for (int i = 0; i < hitCount; i++)
            {
                float distance = Vector3.Distance(_transform.position, _hitColliders[i].transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _nearestEnemy = _hitColliders[i].transform;
                }
            }

            _targetEnemy = _nearestEnemy;
        }

        private void TurnToTargetEnemy()
        {
            Vector3 direction = (_targetEnemy.position - _transform.position);

            Turn(direction);
        }

        private void TurnToSpotRotation()
        {
            if (_companion.Spot == null)
                return;
            
            Vector3 direction = _companion.Spot.forward;
            
            Turn(direction);
        }

        private void Turn(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            
            _rotationToTarget = _transform.rotation.y - lookRotation.y;
        }

        private void TryShoot()
        {
            _animator.SetBool("IsAim", _targetEnemy != null);
        }
    }
}
