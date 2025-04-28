using UnityEngine;

namespace MythicalBattles
{
    public class RangeEnemyMover : EnemyMover, IRandomlyMover
    {
        private const float MoveDurationDeviation = 0.5f;
        
        [SerializeField] private float _moveDuration;
        [SerializeField] private float _directionChangeInterval;
        [SerializeField] private float _raycastDistance;
        [SerializeField] private float _stopDuration;

        private RandomMovementLogic _randomMovementLogic;
        private float _stopTimer;
        private float _deviatedMoveDuration;
        private bool _isMoving;

        public void StopRandomMoving()
        {
            _isMoving = false;
        }

        public void ResetStopTimer()
        {
            _stopTimer = 0f;
        }

        protected override void OnAwake()
        {
            base.OnAwake();

            _randomMovementLogic = new RandomMovementLogic(this, Transform, _moveDuration, _directionChangeInterval,
                _raycastDistance);
            
            float minMoveDuration = _moveDuration - MoveDurationDeviation;
            float maxMoveDuration = _moveDuration + MoveDurationDeviation;

            _deviatedMoveDuration = Random.Range(minMoveDuration, maxMoveDuration);
        }

        protected override void OnEnableBehaviour()
        {
            base.OnEnableBehaviour();
            
            _isMoving = true;
            
            Animator.SetBool(Constants.IsMove, true);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
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
        }
        
        private void Shoot()
        {
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
