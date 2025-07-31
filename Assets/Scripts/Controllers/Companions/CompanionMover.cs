using System.Collections;
using UnityEngine;

namespace MythicalBattles.Companions
{
    public class CompanionMover : MonoBehaviour
    {
        [SerializeField] private float _smoothTime = 0.1f; 
        [SerializeField] private float _maxSpeed = 9f;
        
        private Transform _transform;
        private Vector3 _velocity = Vector3.zero;
        private Coroutine _movementCoroutine;

        private bool _isActive;
        
        public Transform Spot { get; private set; }
        
        private void Awake()
        {
            _transform = transform;
        }

        private void OnDisable()
        {
            _isActive = false;
        }

        public void Move(Transform target)
        {
            _transform.position = target.position;
            Spot = target;
            _isActive = true;
            
            _movementCoroutine = StartCoroutine(MovementRoutine(target));
        }
        
        private IEnumerator MovementRoutine(Transform target)
        {
            while (_isActive)
            {
  
                _transform.position = Vector3.SmoothDamp(
                    current: transform.position,
                    target: target.position,
                    currentVelocity: ref _velocity,
                    smoothTime: _smoothTime,
                    maxSpeed: _maxSpeed
                );

                yield return null; 
            }
            
            StopCoroutine(_movementCoroutine);
        }
    }
}
