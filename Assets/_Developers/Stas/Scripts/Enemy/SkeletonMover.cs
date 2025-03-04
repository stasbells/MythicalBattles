using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator), typeof(Transform))]
    public class SkeletonMover : MonoBehaviour
    {
        private readonly int _defaultLayer = 0;

        [SerializeField] protected float _moveSpeed = 3.0f;
        [SerializeField] private int _damage;

        [SerializeField] private Transform _player;
        [SerializeField] private float _attackDistance = 2.0f;

        private Transform _transform;
        private Animator _animator;
        private CapsuleCollider _capsuleCollider;

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
                Die();

                return;
            }

            if (GetDistanceToPlayer() <= _attackDistance)
                Attack();
            else
                MoveTowardsPlayer();
        }

        public void Damage()
        {
            _player.GetComponent<Health>().TakeDamage(_damage);
        }

        private void Attack()
        {
            _animator.SetBool(Constants.IsAttack, true);
        }

        private void Die()
        {
            gameObject.layer = _defaultLayer;
            _capsuleCollider.enabled = false;
        }

        private void MoveTowardsPlayer()
        {
            RotateTowardsPlayer();

            _animator.SetBool(Constants.IsAttack, false);

            _transform.position += _moveSpeed * Time.deltaTime * GetDirection();
        }

        private float GetDistanceToPlayer()
        {
            return Vector3.Distance(_transform.position, _player.position);
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