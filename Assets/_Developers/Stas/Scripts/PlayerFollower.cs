using System;
using UnityEngine;

namespace MythicalBattles
{
    public class PlayerFollower : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private float _trackingStartDistance = 5f;
        [SerializeField] private float _offset = -10f;

        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();

            SetStartPosition();
        }

        void LateUpdate()
        {
            if (_player == null)
            {
                Debug.LogWarning("Target is not assigned for the camera to follow");
                return;
            }

            if (GetCurrenåDistance() > _trackingStartDistance)
                Follow();
        }

        private void Follow()
        {
            Vector3 targetPosition = new(_transform.position.x, _transform.position.y, _player.position.z + _offset);
            Vector3 smoothedPosition = Vector3.Lerp(_transform.position, targetPosition, _smoothSpeed * Time.deltaTime);

            _transform.position = smoothedPosition;
        }

        private void SetStartPosition()
        {
            _transform.position = new Vector3(_transform.position.x, _transform.position.y, _player.position.z + _offset);
        }

        private float GetCurrenåDistance()
        {
            return Math.Abs(_player.position.z + _offset - _transform.position.z);
        }
    }
}