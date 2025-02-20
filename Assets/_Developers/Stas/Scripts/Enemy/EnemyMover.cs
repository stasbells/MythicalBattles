using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator), typeof(Transform))]
    public abstract class EnemyMover : MonoBehaviour
    {
        private readonly int _isAttack = Animator.StringToHash("isAttack");
        private readonly int _isDead = Animator.StringToHash("isDead");
        private readonly int _defaultLayer = 0;

        [SerializeField] protected float _moveSpeed = 3.0f;

        [SerializeField] private Transform _player;
        [SerializeField] private float _attackRange = 2.0f;
        [SerializeField] private float _attackCooldown = 2.0f;

        private Transform _transform;
        private Animator _animator;
        private float _attackTimer;
        private CapsuleCollider _capsuleCollider;

        private void Awake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_animator.GetBool(_isDead) == true)
            {
                gameObject.layer = _defaultLayer;
                _capsuleCollider.enabled = false;

                return;
            }

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