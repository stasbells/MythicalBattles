using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies.Movers
{
    public class RangeEnemyMover : EnemyMover, IRandomlyMover
    {
        [SerializeField] private float _moveDuration;
        [SerializeField] private float _directionChangeInterval;
        [SerializeField] private float _raycastDistance;
        [SerializeField] private float _stopDuration;

        private RandomMovementLogic _randomMovementLogic;
        private float _stopTimer;
        private bool _isMoving;

        public void StopRandomMoving()
        {
            _isMoving = false;
        }

        public void ResetStopTimer()
        {
            _stopTimer = 0f;
        }

        protected override void OnEnemyMoverAwake()
        {
            _randomMovementLogic = new RandomMovementLogic(this, Transform, _moveDuration,
                _directionChangeInterval, _raycastDistance);

            OnRangeEnemyMoverAwake();
        }

        protected override void OnEnemyMoverEnable()
        {
            _isMoving = true;
            Animator.SetBool(Constants.IsMove, true);

            OnRangeEnemyMoverEnable();
        }

        protected override void OnEnemyMoverFixedUpdate()
        {
            if (Animator.GetBool(Constants.IsDead))
                return;

            if (_isMoving)
                _randomMovementLogic.MoveRandomly();
            else
                Shoot();
        }

        protected virtual void Attack()
        {
            Animator.SetBool(Constants.IsAttack, true);
            Animator.SetBool(Constants.IsMove, false);

            OnRangeEnemyMoverAttack();
        }

        protected virtual void OnRangeEnemyMoverAwake() { }

        protected virtual void OnRangeEnemyMoverEnable() { }

        protected virtual void OnRangeEnemyMoverAttack() { }

        private void Shoot()
        {
            _isMoving = false;

            _stopTimer += Time.deltaTime;

            if (_stopTimer >= _stopDuration)
            {
                _isMoving = true;
                _randomMovementLogic.ResetMoveTimer();
            }

            Attack();
        }
    }
}