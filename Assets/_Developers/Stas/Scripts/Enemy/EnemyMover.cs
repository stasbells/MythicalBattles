using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator), typeof(Transform))]
    public abstract class EnemyMover : MonoBehaviour
    {
        [SerializeField] protected float MoveSpeed = 3.0f;

        private readonly int _isAttack = Animator.StringToHash("isAttack");

        [SerializeField] private Transform _player;
        [SerializeField] private float _attackRange = 2.0f;
        [SerializeField] private float _attackCooldown = 2.0f;

        private Transform _transform;
        private Animator _animator;
        private float _attackTimer;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            float distanceToPlayer = Vector3.Distance(_transform.position, _player.position);

            if (distanceToPlayer <= _attackRange || _attackTimer > 0f)
            {
                if (_attackTimer >= _attackCooldown)
                {
                    _attackTimer = 0f;
                }
                else
                {
                    _attackTimer += Time.deltaTime;
                    Attack();
                }
            }
            else
            {
                MoveTowardsPlayer();
            }
        }

        protected virtual void Attack()
        {
            _animator.SetBool(_isAttack, true);
        }

        protected virtual void MoveTowardsPlayer()
        {
            _animator.SetBool(_isAttack, false);

            RotateTowardsPlayer();

            _transform.position += MoveSpeed * Time.deltaTime * GetDirection();
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