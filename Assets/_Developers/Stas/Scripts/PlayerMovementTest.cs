using UnityEngine;
using UnityEngine.Events;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator), typeof(Transform), typeof(CharacterController))]
    public class PlayerMovementTest : MonoBehaviour
    {
        private readonly int _isMove = Animator.StringToHash("Move");
        private readonly int _isShoot = Animator.StringToHash("Shoot");

        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private float _smoothInputSpeed = 0.2f;

        private Controls _controls;
        private Animator _animator;
        private Transform _transform;
        protected CharacterController _controller;

        private Vector2 _moveDirection;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;

        public UnityAction Shooting;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();

            _animator.SetBool(_isShoot, false);
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

        private void Start()
        {
            Shooting.Invoke();
        }

        private void Update()
        {
            _moveDirection = _controls.Player.Move.ReadValue<Vector2>();

            Move();
        }

        private void Move()
        {
            if (_moveDirection.sqrMagnitude < 0.1f)
            {
                _animator.SetBool(_isMove, false);
               

                return;
            }

            if (_animator.GetBool(_isMove) == false)
                _animator.SetBool(_isMove, true);

            _currentInputVector = Vector2.SmoothDamp(_currentInputVector, _moveDirection, ref _smoothInputVelocity, _smoothInputSpeed);

            float rotationAngle = Mathf.Atan2(_currentInputVector.x, _currentInputVector.y) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

            Vector3 move = new Vector3(_moveDirection.x, 0, _moveDirection.y);
            _controller.Move(_moveSpeed * Time.deltaTime * move);
            
        }
    }
}