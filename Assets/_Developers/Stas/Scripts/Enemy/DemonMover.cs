using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MythicalBattles
{
    public class DemonMover : MonoBehaviour, IDamageDealComponent
    {
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _moveDuration = 2f;
        [SerializeField] private float _directionChangeInterval = 0.5f;
        [SerializeField] private float _raycastDistance = 1f;
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _initDamage;
        [SerializeField] private float _playerSearchRadius = 50f;
        [SerializeField] ParticleSystem _effect;
        
        private Transform _transform;
        private Transform _player;
        private Animator _animator;
        private CapsuleCollider _capsuleCollider;
        private Vector3 _randomDirection;

        private float _damage;
        private float _moveTimer;
        private float _attackTimer;
        private float _directionChangeTimer;
        private bool _isMovingAway = false;

        private void Awake()
        {
            _effect.Stop();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
        }
        
        private void OnEnable()
        {
            gameObject.layer = Constants.LayerEnemy;
            _capsuleCollider.enabled = true;
        }

        private void Start()
        {
            if(TryFindPlayer() == false)
                throw new InvalidOperationException();
        }

        private void Update()
        {
            if (_animator.GetBool(Constants.IsDead))
            {
                gameObject.layer = Constants.LayerDefault;
                _capsuleCollider.enabled = false;

                return;
            }

            float distanceToPlayer = Vector3.Distance(_transform.position, _player.position);

            if (distanceToPlayer <= _attackRange && !_isMovingAway || _animator.GetBool(Constants.IsShoot))
            {
                if (_attackTimer >= _attackCooldown)
                {
                    _effect.Stop();
                    _attackTimer = 0f;
                    _isMovingAway = true;
                    _animator.SetBool(Constants.IsShoot, false);
                }
                else
                {
                    _attackTimer += Time.deltaTime;
                    _animator.SetBool(Constants.IsShoot, true);
                }
            }
            else if (_isMovingAway)
            {
                MoveRandomly();
            }
            else if (!_animator.GetBool(Constants.IsAttack))
            {
                MoveTo(GetDirectionToPlayer());
            }
            else
            {
                RotateTowards(GetDirectionToPlayer());
            }
        }
        
        private bool TryFindPlayer()
        {
            Collider[] colliders = new Collider[1];
            
            int hitCount = Physics.OverlapSphereNonAlloc(_transform.position, _playerSearchRadius, colliders, Constants.MaskLayerPlayer);

            if (hitCount == 0)
                return false;
            
            _player = colliders[0].transform;

            return true;
        }

        private void MoveRandomly()
        {
            _moveTimer += Time.deltaTime;
            _directionChangeTimer += Time.deltaTime;

            if (_moveTimer >= _moveDuration)
            {
                _moveTimer = 0f;
                _isMovingAway = false;
                _animator.SetBool(Constants.IsAttack, true);
                _animator.SetBool(Constants.IsMove, false);
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

        public void Attack()
        {
            _player.GetComponent<Health>().TakeDamage(_damage);
        }

        public void AttackEffect()
        {
            _effect.Play();
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
            _animator.SetBool(Constants.IsShoot, false);
            _animator.SetBool(Constants.IsAttack, false);

            _transform.position += _moveSpeed * Time.deltaTime * direction;

            RotateTowards(direction);
        }

        private bool TryFindObstacleIn(Vector3 direction)
        {
            if (Physics.Raycast(_transform.position, direction, out _, _raycastDistance, Constants.MaskLayerObstacles))
            {
                Debug.DrawRay(_transform.position, direction * _raycastDistance, Color.red, 1f);

                return true;
            }

            Debug.DrawRay(_transform.position, direction * _raycastDistance, Color.green, 1f);

            return false;
        }

        public void ApplyWaveDamageMultiplier(float multiplier)
        {
            _damage = _initDamage * multiplier;
        }

        public void CancelWaveDamageMultiplier()
        {
            _damage = _initDamage;
        }
    }
}
