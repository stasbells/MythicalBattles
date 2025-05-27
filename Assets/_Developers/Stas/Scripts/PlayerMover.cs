using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator), typeof(Transform), typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private float _smoothInputSpeed = 0.2f;

        protected CharacterController _controller;
        private CapsuleCollider _capsuleCollider;
        private Controls _controls;
        private Animator _animator;
        private Transform _transform;

        private Vector2 _moveDirection;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;
        private Vector2 _rotateInput = new Vector2(0.7f, 0.7f);
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _controls ??= new Controls();
            _controls.Player.Enable();
        }

        public void OnDisable()
        {
            _controls.Player.Disable();
        }

        private void Update()
        {
            if (_animator.GetBool(Constants.IsDead))
            {
                Die();

                return;
            }

            Move();
        }

        private void SetMovingState(bool value)
        {
            _animator.SetBool(Constants.IsMove, value);
        }

        private void Move()
        {
            _moveDirection = _controls.Player.Move.ReadValue<Vector2>();
            _moveDirection = Quaternion.AngleAxis(Constants.MoveControllerRotationAngle, Vector3.forward) * _moveDirection;

            if (_moveDirection.sqrMagnitude < 0.1f)
            {
                SetMovingState(false);

                return;
            }

            if (!_animator.GetBool(Constants.IsMove))
                SetMovingState(true);

            _currentInputVector = Vector2.SmoothDamp(_currentInputVector, _moveDirection, ref _smoothInputVelocity, _smoothInputSpeed);

            float rotationAngle = Mathf.Atan2(_currentInputVector.x, _currentInputVector.y) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

            Vector3 move = new(_moveDirection.x, 0, _moveDirection.y);
            _controller.Move(_moveSpeed * Time.deltaTime * move);
        }
        private void Die()
        {
            _animator.SetBool(Constants.IsMove, false);
            _animator.SetBool(Constants.IsAttack, false);
            _capsuleCollider.enabled = false;
            gameObject.layer = Constants.LayerDefault;
        }
    }
}