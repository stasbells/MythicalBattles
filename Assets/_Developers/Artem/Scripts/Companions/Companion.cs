using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    public class Companion : MonoBehaviour
    {
        [SerializeField] private float _smoothTime = 0.1f; 
        [SerializeField] private float _maxSpeed = 8f;
        
        private Transform _transform;
        private Vector3 _velocity = Vector3.zero;
        private Coroutine _movementCoroutine;

        public bool IsActive { get; private set; }
        
        public Transform Spot { get; private set; }
        
        private void Awake()
        {
            _transform = transform;
        }

        private void OnDestroy()
        {
            IsActive = false;
        }

        public void Move(Transform target)
        {
            _transform.position = target.position;
            Spot = target;
            IsActive = true;
            
            _movementCoroutine = StartCoroutine(MovementRoutine(target));
        }
        
        private IEnumerator MovementRoutine(Transform target)
        {
            while (IsActive)
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
