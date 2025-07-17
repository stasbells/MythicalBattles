using UnityEngine;

namespace MythicalBattles
{
    public class MeleeEnemyMover : EnemyMover, IWaveDamageMultiplier
    {
        private const float MaxDistanceToDealDamage = 4;
        
        [SerializeField] protected float InitDamage;
        [SerializeField] protected float AttackDistance;

        private float _damage;
        
        protected override void OnAwake()
        {
            base.OnAwake();
            
            _damage = InitDamage;
        }
        
        public void Attack()
        {
            float distanceToPlayer = Vector3.Distance(Transform.position, Player.position);
            
            if(distanceToPlayer < MaxDistanceToDealDamage)
                Player.GetComponent<Health>().TakeDamage(_damage);
        }
        
        public void ApplyMultiplier(float multiplier)
        {
            _damage = InitDamage * multiplier;
        }

        public void CancelMultiplier()
        {
            _damage = InitDamage;
        }
        
        protected float GetDistanceToPlayer()
        {
            return Vector3.Distance(Transform.position, Player.position);
        }
    }
}