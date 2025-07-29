using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using System;
using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Health))]
    public abstract class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _playerSearchRadius = 50f;
        [SerializeField] private float _rotationSpeed = 10f;

        private CapsuleCollider _capsuleCollider;
        private Transform _playerTransform;
        private Transform _transform;
        private Animator _animator;

        public float MoveSpeed => _moveSpeed;
        public Transform PlayerTransform => _playerTransform;
        public Transform Transform => _transform;
        public Animator Animator => _animator;

        private void Awake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();

            OnEnemyMoverAwake();
        }

        private void OnEnable()
        {
            gameObject.layer = Constants.LayerEnemy;
            _capsuleCollider.enabled = true;

            OnEnemyMoverEnable();
        }

        private void Start()
        {
            if (TryFindPlayer() == false)
                throw new InvalidOperationException();

            OnEnemyMoverStart();
        }

        private void FixedUpdate()
        {
            if (_animator.GetBool(Constants.IsDead))
            {
                gameObject.layer = Constants.LayerDefault;
                _capsuleCollider.enabled = false;
            }

            OnEnemyMoverFixedUpdate();
        }

        public void MoveTo(Vector3 direction)
        {
            _animator.SetBool(Constants.IsAttack, false);
            _animator.SetBool(Constants.IsMove, true);

            RotateTowards(direction);

            _transform.position += Time.deltaTime * MoveSpeed * direction;
        }

        protected virtual void OnEnemyMoverAwake() { }

        protected virtual void OnEnemyMoverEnable() { }

        protected virtual void OnEnemyMoverStart() { }

        protected virtual void OnEnemyMoverFixedUpdate() { }

        protected Vector3 GetDirectionToPlayer()
        {
            return (_playerTransform.position - _transform.position).normalized;
        }

        protected void RotateTowards(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation,
                lookRotation, Time.deltaTime * _rotationSpeed);
        }

        private bool TryFindPlayer()
        {
            Collider[] colliders = new Collider[1];

            int hitCount = Physics.OverlapSphereNonAlloc(_transform.position,
                _playerSearchRadius, colliders, Constants.MaskLayerPlayer);

            if (hitCount == 0)
                return false;

            _playerTransform = colliders[0].transform;

            return true;
        }
    }
}