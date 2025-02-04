using UnityEngine;
using Synty.AnimationBaseLocomotion.Samples;

namespace MythicalBattles
{
    public class PlayerAutoAim : MonoBehaviour
    {
        [SerializeField] private SamplePlayerAnimationController _controller;
        [SerializeField] private float _aimRadius = 10f;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _rotationSpeed = 5f;

        private Transform _targetEnemy;
        private Collider[] _hitColliders;

        void Update()
        {
            FindNearestEnemy();

            if (_controller.IsStopped & _targetEnemy != null)
                TurnToTargetEnemy();
        }

        void FindNearestEnemy()
        {
            _hitColliders = Physics.OverlapSphere(transform.position, _aimRadius, _enemyLayer);
            float closestDistance = Mathf.Infinity;
            Transform nearestEnemy = null;

            foreach (var hitCollider in _hitColliders)
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = hitCollider.transform;
                }
            }

            _targetEnemy = nearestEnemy;
        }

        private void TurnToTargetEnemy()
        {
            Vector3 direction = (_targetEnemy.position - transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _aimRadius);
        }
    }
}