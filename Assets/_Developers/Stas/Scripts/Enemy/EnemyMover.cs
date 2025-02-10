using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator), typeof(Transform))]
    public abstract class EnemyMover : MonoBehaviour
    {
        private readonly int _isAttack = Animator.StringToHash("isAttack");

        [Header("Target")]
        [SerializeField] private Transform _player;

        [Header("Parameters")]
        [SerializeField] private float _moveSpeed = 3.0f;
        [SerializeField] private float _attackRange = 2.0f;
        [SerializeField] private int _attackDamage = 10;
        [SerializeField] private float _attackCooldown = 2.0f;

        private Transform _transform;
        private Animator _animator;
        private float _lastAttackTime = 0f;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_player == null)
            {
                Debug.LogWarning("Player reference is not set in EnemyMover script.");
                return;
            }

            float distanceToPlayer = Vector3.Distance(_transform.position, _player.position);

            if (distanceToPlayer <= _attackRange)
            {
                if (Time.time - _lastAttackTime >= _attackCooldown)
                {
                    Attack();
                    _lastAttackTime = Time.time;
                }
            }
            else
            {
                MoveTowardsPlayer();
            }

            RotateTowardsPlayer();
        }

        protected virtual void Attack()
        {
            _animator.SetBool(_isAttack, true);

            Debug.Log("Enemy attacks player for " + _attackDamage + " damage!");
        }

        protected virtual void MoveTowardsPlayer()
        {
            _animator.SetBool(_isAttack, false);

            _transform.position += _moveSpeed * Time.deltaTime * GetDirection();
        }

        private void RotateTowardsPlayer()
        {
            Quaternion lookRotation = Quaternion.LookRotation(GetDirection());
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * 10f);
        }

        private Vector3 GetDirection()
        {
            return (_player.position - _transform.position).normalized;
        }
    }
}