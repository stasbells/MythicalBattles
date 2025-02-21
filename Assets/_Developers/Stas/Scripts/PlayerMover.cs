using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator), typeof(Transform), typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private readonly int _isMove = Animator.StringToHash("isMove");
        private readonly int _isShoot = Animator.StringToHash("isShoot");

        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private float _smoothInputSpeed = 0.2f;

        protected CharacterController _controller;
        private Controls _controls;
        private Animator _animator;
        private Transform _transform;

        private Vector2 _moveDirection;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
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
            Move();
        }

        private void SetMovingState(bool value)
        {
            _animator.SetBool(_isMove, value);
            _animator.SetBool(_isShoot, !value);
        }

        private void Move()
        {
            _moveDirection = _controls.Player.Move.ReadValue<Vector2>();

            if (_moveDirection.sqrMagnitude < 0.1f)
            {
                SetMovingState(false);

                return;
            }

            if (_animator.GetBool(_isMove) == false)
                SetMovingState(true);

            _currentInputVector = Vector2.SmoothDamp(_currentInputVector, _moveDirection, ref _smoothInputVelocity, _smoothInputSpeed);

            float rotationAngle = Mathf.Atan2(_currentInputVector.x, _currentInputVector.y) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

            Vector3 move = new(_moveDirection.x, 0, _moveDirection.y);
            _controller.Move(_moveSpeed * Time.deltaTime * move);
        }
    }
}