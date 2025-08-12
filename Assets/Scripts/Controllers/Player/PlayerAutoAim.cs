using DG.Tweening;
using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAutoAim : MonoBehaviour
    {
        private readonly Collider[] _hitColliders = new Collider[10];
        private readonly float _maxAimAngle = 0.05f;

        [SerializeField] private AimMarker _aimMarker;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _aimRadius;

        private Animator _animator;
        private Transform _nearestEnemy;
        private Transform _targetEnemy;
        private Transform _transform;
        private Transform _aimMarkerTransform;
        private Vector3 _aimMarkerScale;

        private float _rotationToTarget;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _aimMarkerTransform = _aimMarker.GetComponent<Transform>();
            _aimMarkerScale = _aimMarkerTransform.localScale;
            _aimMarker.gameObject.SetActive(false);
            _transform = transform;
        }

        private void Update()
        {
            if (_animator.GetBool(Constants.IsDead))
                return;

            FindNearestEnemy();

            if (_targetEnemy)
                TurnToTargetEnemy();
            else
                SetActiveTargetMarker(false);

            TakeAim();
        }

        private void FindNearestEnemy()
        {
            float closestDistance = Mathf.Infinity;
            _nearestEnemy = null;

            int hitCount = Physics.OverlapSphereNonAlloc(_transform.position, _aimRadius, _hitColliders, Constants.MaskLayerEnemy);

            for (int i = 0; i < hitCount; i++)
            {
                float distance = Vector3.Distance(_transform.position, _hitColliders[i].transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _nearestEnemy = _hitColliders[i].transform;
                }
            }

            _targetEnemy = _nearestEnemy;
        }

        private void TurnToTargetEnemy()
        {
            Vector3 direction = _targetEnemy.position - _transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            _rotationToTarget = _transform.rotation.y - lookRotation.y;

            MarkTarget();
        }

        private void TakeAim()
        {
            _animator.SetBool(Constants.IsAttack, _targetEnemy != null && Mathf.Abs(_rotationToTarget) < _maxAimAngle);
        }

        private void MarkTarget()
        {
            if (_aimMarker.transform.parent != _targetEnemy.transform)
            {
                _aimMarker.transform.parent = _targetEnemy.transform;
                _aimMarker.transform.position = _targetEnemy.position;

                _aimMarker.transform.localScale = Vector3.zero;
                _aimMarker.transform.DOScale(_aimMarkerScale, 0.2f);
            }

            SetActiveTargetMarker(true);
        }

        private void SetActiveTargetMarker(bool isActive)
        {
            if (!isActive)
                _aimMarker.transform.parent = _transform;

            _aimMarker.gameObject.SetActive(isActive);
        }
    }
}