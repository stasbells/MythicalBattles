using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies.Movers
{
    public class MeleeEnemyMover : EnemyMover, IWaveDamageMultiplier
    {
        private const float MaxDistanceToDealDamage = 4f;

        [SerializeField] private float _initDamage;
        [SerializeField] private float _attackDistance;

        private float _damage;

        public float InitDamage => _initDamage;
        public float AttackDistance => _attackDistance;

        public void OnAttackAnimationPlay()
        {
            float distanceToPlayer = Vector3.Distance(Transform.position, PlayerTransform.position);

            if (distanceToPlayer < MaxDistanceToDealDamage)
                PlayerTransform.GetComponent<Health>().TakeDamage(_damage);
        }

        public void ApplyMultiplier(float multiplier)
        {
            _damage = InitDamage * multiplier;
        }

        public void CancelMultiplier()
        {
            _damage = InitDamage;
        }

        protected override void OnEnemyMoverAwake()
        {
            _damage = InitDamage;

            OnMeleeEnemyMoverAwake();
        }

        protected override void OnEnemyMoverStart()
        {
            OnMeleeEnemyMoverStart();
        }

        protected override void OnEnemyMoverEnable()
        {
            OnMeleeEnemyMoverEnable();
        }

        protected override void OnEnemyMoverFixedUpdate()
        {
            OnMeleeEnemyMoverFixedUpdate();
        }

        protected float GetDistanceToPlayer()
        {
            return Vector3.Distance(Transform.position, PlayerTransform.position);
        }

        protected virtual void OnMeleeEnemyMoverAwake() { }

        protected virtual void OnMeleeEnemyMoverStart() { }

        protected virtual void OnMeleeEnemyMoverFixedUpdate() { }

        protected virtual void OnMeleeEnemyMoverEnable() { }
    }
}