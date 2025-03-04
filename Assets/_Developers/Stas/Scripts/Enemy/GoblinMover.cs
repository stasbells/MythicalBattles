using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Transform), typeof(Animator))]
    public class GoblinMover : MonoBehaviour
    {
        private readonly int _defaultLayer = 0;

        [SerializeField] private Transform _player;
        [SerializeField] private LayerMask _obstacleLayer;

        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _moveDuration = 2f;
        [SerializeField] private float _directionChangeInterval = 0.5f;
        [SerializeField] private float _raycastDistance = 1f;
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private int _damage;

        private Transform _transform;
        private Animator _animator;
        private CapsuleCollider _capsuleCollider;
        private Vector3 _randomDirection;

        private float _moveTimer;
        private float _attackTimer;
        private float _directionChangeTimer;
        private bool _isMovingAway = false;

        private void Awake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_animator.GetBool(Constants.IsDead))
            {
                gameObject.layer = _defaultLayer;
                _capsuleCollider.enabled = false;

                return;
            }

            float distanceToPlayer = Vector3.Distance(_transform.position, _player.position);

            if (distanceToPlayer <= _attackRange && !_isMovingAway)
            {
                if (_attackTimer >= _attackCooldown)
                {
                    _attackTimer = 0f;
                    _isMovingAway = true;
                }
                else
                {
                    _attackTimer += Time.deltaTime;
                    _animator.SetBool(Constants.IsAttack, true);
                }
            }
            else if (_isMovingAway)
            {
                MoveRandomly();
            }
            else
            {
                MoveTo(GetDirectionToPlayer());
            }
        }

        private void MoveRandomly()
        {
            _moveTimer += Time.deltaTime;
            _directionChangeTimer += Time.deltaTime;

            if (_moveTimer >= _moveDuration)
            {
                _moveTimer = 0f;
                _isMovingAway = false;
            }
            else
            {
                if (_directionChangeTimer >= _directionChangeInterval)
                {
                    _directionChangeTimer = 0f;
                    _randomDirection = GetFreeRandomDirection();
                }

                if(_randomDirection == Vector3.zero)
                    _randomDirection = GetFreeRandomDirection();

                MoveTo(_randomDirection);
            }
        }

        public void Attack()
        {
            _player.GetComponent<Health>().TakeDamage(_damage);
        }

        private Vector3 GetFreeRandomDirection()
        {
            Vector3 direction = GetRandomDirection();

            while (TryFindObstacleIn(direction))
                direction = GetRandomDirection();

            return direction;
        }

        private Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }

        private void RotateTowards(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        private Vector3 GetDirectionToPlayer()
        {
            return (_player.position - _transform.position).normalized;
        }

        private void MoveTo(Vector3 direction)
        {
            _animator.SetBool(Constants.IsAttack, false);

            _transform.position += _moveSpeed * Time.deltaTime * direction;

            RotateTowards(direction);
        }

        private bool TryFindObstacleIn(Vector3 direction)
        {
            if (Physics.Raycast(_transform.position, direction, out _, _raycastDistance, _obstacleLayer))
            {
                Debug.DrawRay(_transform.position, direction * _raycastDistance, Color.red, 1f);

                return true;
            }

            Debug.DrawRay(_transform.position, direction * _raycastDistance, Color.green, 1f);

            return false;
        }
    }
}