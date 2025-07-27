using System;
using UnityEngine;

namespace MythicalBattles.Companions
{
    [RequireComponent(typeof(CompanionMover))]
    [RequireComponent(typeof(Animator))]
    public class CompanionAutoAim : MonoBehaviour
    {
        private const int CollidersToCheckCount = 10;
        
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _rotationSpeed = 500f;
        [SerializeField] private float _aimRadius = 15f;

        private CompanionMover _companion;
        private Animator _animator;
        private Transform _nearestEnemy;
        private Transform _targetEnemy;
        private Transform _transform;
        private float _rotationToTarget;
        private Collider[] _hitColliders;

        public event Action EnemyFound;
        public event Action EnemyMissed;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _companion = GetComponent<CompanionMover>();
            _transform = transform;
        }

        private void Update()
        {
            FindNearestEnemy();

            if (_targetEnemy)
                TurnToTargetEnemy();
            else
                TurnToSpotRotation();
            
            AdjustAnimation();
        }

        private void FindNearestEnemy()
        {
            float closestDistance = Mathf.Infinity;
            
            _nearestEnemy = null;
            _hitColliders = new Collider[CollidersToCheckCount];
            
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
            Vector3 directionToEnemyPosition = _targetEnemy.position - _transform.position;
            
            Vector3 newDirection = new Vector3(directionToEnemyPosition.x, 0, directionToEnemyPosition.z);

            Turn(newDirection);
        }

        private void TurnToSpotRotation()
        {
            if (_companion.Spot == null)
                return;
            
            Vector3 newDirection = _companion.Spot.forward;
            
            Turn(newDirection);
        }

        private void Turn(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            
            _transform.rotation = Quaternion.RotateTowards(
                _transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            
            _rotationToTarget = _transform.rotation.y - lookRotation.y;
        }

        private void AdjustAnimation()
        {
            if (_targetEnemy)
            {
                if (_animator.GetBool("IsAttack")) 
                    return;
                
                EnemyFound?.Invoke();
                    
                _animator.SetBool("IsAttack", true);
            }
            else
            {
                if (_animator.GetBool("IsAttack") == false) 
                    return;
                
                EnemyMissed?.Invoke();
                    
                _animator.SetBool("IsAttack", false);
            }
        }
    }
}
