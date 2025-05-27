using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using System;
using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Health))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] protected float MoveSpeed;
        [SerializeField] protected float PlayerSearchRadius = 50f;
        [SerializeField] private float RotationSpeed = 10f;
        
        protected Transform Player;
        protected Animator Animator;
        protected Transform Transform;
        private CapsuleCollider _capsuleCollider;

        private void Awake()
        {
            OnAwake();
        }
        
        private void OnEnable()
        {
            OnEnableBehaviour();
        }
        
        private void Start()
        {
            OnStart();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }
        

        protected virtual void OnAwake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            Transform = GetComponent<Transform>();
            Animator = GetComponent<Animator>();
        }

        protected virtual void OnEnableBehaviour()
        {
            gameObject.layer = Constants.LayerEnemy;
            _capsuleCollider.enabled = true;
        }

        protected virtual void OnStart()
        {
            if(TryFindPlayer() == false)
                throw new InvalidOperationException();
        }

        protected virtual void OnFixedUpdate()
        {
            if (Animator.GetBool(Constants.IsDead))
            {
                gameObject.layer = Constants.LayerDefault;
                _capsuleCollider.enabled = false;
            }
        }
        
        public void MoveTo(Vector3 direction)
        {
            Animator.SetBool(Constants.IsAttack, false);
            Animator.SetBool(Constants.IsMove, true);

            RotateTowards(direction);

            Transform.position += Time.deltaTime * MoveSpeed * direction;
        }
        
        protected Vector3 GetDirectionToPlayer()
        {
            return (Player.position - Transform.position).normalized;
        }
        
        protected void RotateTowards(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Transform.rotation = Quaternion.Slerp(Transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        }
        
        private bool TryFindPlayer()
        {
            Collider[] colliders = new Collider[1];
            
            int hitCount = Physics.OverlapSphereNonAlloc(Transform.position, PlayerSearchRadius, colliders, Constants.MaskLayerPlayer);

            if (hitCount == 0)
                return false;
            
            Player = colliders[0].transform;

            return true;
        }
    }
}
