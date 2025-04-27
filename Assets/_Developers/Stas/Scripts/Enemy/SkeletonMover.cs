using System;
using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator), typeof(Transform))]
    public class SkeletonMover : MonoBehaviour, IWaveDamageMultiplier
    {
        [SerializeField] private float _moveSpeed = 3.0f;
        [SerializeField] private float _initDamage;
        [SerializeField] private float _attackDistance = 2.0f;
        [SerializeField] private float _playerSearchRadius = 50f;

        private float _damage;
        private Transform _transform;
        private Transform _player;
        private Animator _animator;
        private CapsuleCollider _capsuleCollider;

        private void Awake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            _damage = _initDamage;
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
                Die();

                return;
            }

            if (GetDistanceToPlayer() <= _attackDistance)
                _animator.SetBool(Constants.IsAttack, true);
            else
                MoveTowardsPlayer();
        }

        public void Damage()
        {
            _player.GetComponent<Health>().TakeDamage(_damage);
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

        private void Attack()
        {
            _player.GetComponent<Health>().TakeDamage(_damage);
        }

        private void Die()
        {
            gameObject.layer = Constants.LayerDefault;
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
        
        public void ApplyMultiplier(float multiplier)
        {
            _damage = _initDamage * multiplier;
        }

        public void CancelMultiplier()
        {
            _damage = _initDamage;
        }
    }
}