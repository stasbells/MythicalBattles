using UnityEngine;

namespace MythicalBattles
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed = 2.0f;

        protected CharacterController Controller;
        protected PlayerActionsExample PlayerInput;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;

        private void Awake()
        {
            Controller = GetComponent<CharacterController>();
            PlayerInput = new PlayerActionsExample();
        }

        private void Update()
        {
            _groundedPlayer = Controller.isGrounded;

            if (_groundedPlayer && _playerVelocity.y < 0)
                _playerVelocity.y = 0f;

            Vector2 movement = PlayerInput.Player.Move.ReadValue<Vector2>();
            Vector3 move = new Vector3(movement.x, 0, movement.y);
            Controller.Move(move * Time.deltaTime * _playerSpeed);

            if (move != Vector3.zero)
                gameObject.transform.forward = move;

            Controller.Move(_playerVelocity * Time.deltaTime);
        }

        private void OnEnable()
        {
            PlayerInput.Enable();
        }

        private void OnDisable()
        {
            PlayerInput.Disable();
        }
    }
}