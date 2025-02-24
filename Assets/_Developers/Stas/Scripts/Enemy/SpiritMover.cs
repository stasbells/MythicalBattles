using UnityEngine;

namespace MythicalBattles
{
    public class SpiritMover : MonoBehaviour
    {
        private readonly int _isAttack = Animator.StringToHash("isAttack");
        private readonly int _isDead = Animator.StringToHash("isDead");
        private readonly int _defaultLayer = 0;

        [SerializeField] private Transform _player;
        [SerializeField] private LayerMask _obstacleLayer;

        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _moveDuration = 2f;
        [SerializeField] private float _directionChangeInterval = 0.5f;
        [SerializeField] private float _raycastDistance = 1f;
        [SerializeField] private float _stopDuration = 1f;
        [SerializeField] private float _rotationSpeed = 10f;

        private Transform _transform;
        private Animator _animator;
        private Vector3 _randomDirection;
        private CapsuleCollider _capsuleCollider;

        private float _moveTimer;
        private float _stopTimer;
        private float _directionChangeTimer;
        private bool _isMoving = true;

        private void Awake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(_animator.GetBool(_isDead) == true)
            {
                gameObject.layer = _defaultLayer;
                _capsuleCollider.enabled = false;

                return;
            }

            if (_isMoving)
                MoveRandomly();
            else
                StopAndShoot();
        }

        private void MoveRandomly()
        {
            _moveTimer += Time.deltaTime;
            _directionChangeTimer += Time.deltaTime;

            if (_moveTimer >= _moveDuration)
            {
                _moveTimer = 0f;
                _stopTimer = 0f;
                _isMoving = false;
            }
            else
            {
                if (_directionChangeTimer >= _directionChangeInterval)
                {
                    _directionChangeTimer = 0f;
                    _randomDirection = GetFreeRandomDirection();
                }

                if (_randomDirection == Vector3.zero)
                    _randomDirection = GetFreeRandomDirection();

                MoveTo(_randomDirection);
            }
        }

        private void StopAndShoot()
        {
            _stopTimer += Time.deltaTime;

            if (_stopTimer >= _stopDuration)
            {
                _isMoving = true;
                _moveTimer = 0f;
            }

            Attack();
        }

        private void Attack()
        {
            _animator.SetBool(_isAttack, true);
        }

        private Vector3 GetFreeRandomDirection()
        {
            Vector3 direction = GetRandomDirection();

            while (IsObstacleIn(direction))
                direction = GetRandomDirection();

            return direction;
        }

        private Vector3 GetRandomDirection()
        {
            Vector3[] directions =
            {
                transform.forward,
                -transform.forward,
                transform.right,
                -transform.right
            };

            return directions[Random.Range(0, directions.Length - 1)].normalized;
        }

        private void RotateTowards(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        private void MoveTo(Vector3 direction)
        {
            _animator.SetBool(_isAttack, false);

            RotateTowards(direction);

            _transform.position += Time.deltaTime * _moveSpeed * direction;
        }

        private bool IsObstacleIn(Vector3 direction)
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
