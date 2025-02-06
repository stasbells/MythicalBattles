using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator))]
    public class EnemyMover : MonoBehaviour
    {
        private readonly int _isAttack = Animator.StringToHash("isAttack");

        [Header("Target")]
        [SerializeField] private Transform _player;

        [Header("Parameters")]
        [SerializeField] private float _moveSpeed = 3.0f;
        [SerializeField] private float _attackRange = 2.0f;
        [SerializeField] private int _attackDamage = 10;
        [SerializeField] private float _attackCooldown = 2.0f;

        private Animator _animator;
        private float _lastAttackTime = 0f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_player == null)
            {
                Debug.LogWarning("Player reference is not set in EnemyMover script.");
                return;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

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

        private void RotateTowardsPlayer()
        {
            Quaternion lookRotation = Quaternion.LookRotation(GetDirection());
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }

        private void MoveTowardsPlayer()
        {
            _animator.SetBool(_isAttack, false);

            transform.position += _moveSpeed * Time.deltaTime * GetDirection();
        }

        private void Attack()
        {
            _animator.SetBool(_isAttack, true);

            Debug.Log("Enemy attacks player for " + _attackDamage + " damage!");
        }

        private Vector3 GetDirection()
        {
            return (_player.position - transform.position).normalized;
        }
    }
}