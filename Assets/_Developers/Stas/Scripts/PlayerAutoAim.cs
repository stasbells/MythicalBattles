using UnityEngine;
using DG.Tweening;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAutoAim : MonoBehaviour
    {
        private readonly int _isAim = Animator.StringToHash("isAim");
        private readonly int _isDead = Animator.StringToHash("isDead");

        [SerializeField] GameObject _aimMarker;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _aimRadius;

        private Collider[] _hitColliders;
        private Animator _animator;
        private Transform _nearestEnemy;
        private Transform _targetEnemy;
        private Transform _transform;
        private Vector3 _aimMarkerScale;

        private float _rotationToTarget;

        private void Awake()
        {
            _aimMarkerScale = _aimMarker.transform.localScale;

            Debug.Log($"{_aimMarkerScale}");

            _aimMarker.SetActive(false);
            _transform = transform;
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_animator.GetBool(_isDead) == true)
                return;

            FindNearestEnemy();

            if (_targetEnemy)
                TurnToTargetEnemy();
            else
                SetActiveTargetMarker(false);

            TryShoot();
        }

        private void FindNearestEnemy()
        {
            float closestDistance = Mathf.Infinity;
            _nearestEnemy = null;

            _hitColliders = Physics.OverlapSphere(_transform.position, _aimRadius, _enemyLayer);

            foreach (var hitCollider in _hitColliders)
            {
                float distance = Vector3.Distance(_transform.position, hitCollider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _nearestEnemy = hitCollider.transform;
                }
            }

            _targetEnemy = _nearestEnemy;
        }

        private void TurnToTargetEnemy()
        {
            Vector3 direction = (_targetEnemy.position - _transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            _rotationToTarget = _transform.rotation.y - lookRotation.y;

            MarkTarget();
        }

        private void TryShoot()
        {
            _animator.SetBool(_isAim, _targetEnemy != null && Mathf.Abs(_rotationToTarget) < 0.1f);
        }

        private void MarkTarget()
        {
            if (_targetEnemy == null)
            {
                SetActiveTargetMarker(false);

                return;
            }

            if (_aimMarker.transform.parent != _targetEnemy.transform)
            {
                _aimMarker.transform.parent = _targetEnemy.transform;
                _aimMarker.transform.position = _targetEnemy.position;

                _aimMarker.transform.localScale = Vector3.zero;
                _aimMarker.transform.DOScale(_aimMarkerScale, 0.3f);
            }

            SetActiveTargetMarker(true);
        }

        private void SetActiveTargetMarker(bool isActive)
        {
            if (!isActive)
                _aimMarker.transform.parent = _transform;

            _aimMarker.SetActive(isActive);
        }
    }
}