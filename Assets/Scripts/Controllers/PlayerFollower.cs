using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers
{
    public class PlayerFollower : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private float _offsetZ = -10f;
        [SerializeField] private float _offsetX = -10f;

        private Transform _playerTransform;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _playerTransform = _player;
        }

        private void Start()
        {
            SetStartPosition();
        }

        private void LateUpdate()
        {
            Follow();
        }

        public void SetTarget(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Follow()
        {
            Vector3 targetPosition = new (
                _playerTransform.position.x + _offsetX,
                _transform.position.y,
                _playerTransform.position.z + _offsetZ);
            
            _transform.position = Vector3.Lerp(_transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        }

        private void SetStartPosition()
        {
            _transform.position = new Vector3(_playerTransform.position.x + _offsetX, _transform.position.y, _playerTransform.position.z + _offsetZ);
        }
    }
}