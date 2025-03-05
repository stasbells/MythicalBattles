using System;
using UnityEngine;

namespace MythicalBattles
{
    public class PlayerFollower : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _smoothSpeed = 0.125f;
        //[SerializeField] private float _trackingStartDistance = 5f;
        [SerializeField] private float _offsetZ = -10f;
        [SerializeField] private float _offsetX = -10f;

        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Start()
        {
            SetStartPosition();
        }

        void LateUpdate()
        {
            //if (GetCurrentDistance() > _trackingStartDistance)

            Follow();
        }

        private void Follow()
        {
            Vector3 targetPosition = new(_playerTransform.position.x + _offsetX, _transform.position.y, _playerTransform.position.z + _offsetZ);
            _transform.position = Vector3.Lerp(_transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        }

        private void SetStartPosition()
        {
            _transform.position = new Vector3(_playerTransform.position.x + _offsetX, _transform.position.y, _playerTransform.position.z + _offsetZ);
        }

        private float GetCurrentDistance()
        {
            return Math.Abs(_playerTransform.position.z + _offsetZ - _transform.position.z);
        }
    }
}