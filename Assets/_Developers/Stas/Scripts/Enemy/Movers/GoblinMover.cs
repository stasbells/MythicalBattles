using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(Animator))]
    public class GoblinMover : MeleeEnemyMover, IRandomlyMover
    {
        [SerializeField] private float _durationOfRandomMove = 2f;
        [SerializeField] private float _directionChangeInterval = 0.5f;
        [SerializeField] private float _raycastDistance = 5f;
        [SerializeField] private float _attackCooldown = 2f;

        private RandomMovementLogic _randomMovementLogic;
        private float _attackTimer;
        private bool _isMovingRandomly;

        public void StopRandomMoving()
        {
            _isMovingRandomly = false;
        }

        protected override void OnMeleeEnemyMoverAwake()
        {
            _randomMovementLogic = new RandomMovementLogic(this, Transform,
                _durationOfRandomMove, _directionChangeInterval, _raycastDistance);
        }

        protected override void OnMeleeEnemyMoverFixedUpdate()
        {
            if (Animator.GetBool(Constants.IsDead))
                return;

            if (GetDistanceToPlayer() <= AttackDistance && !_isMovingRandomly)
            {
                if (_attackTimer >= _attackCooldown)
                {
                    _attackTimer = 0f;
                    _isMovingRandomly = true;
                }
                else
                {
                    _attackTimer += Time.deltaTime;
                    Animator.SetBool(Constants.IsAttack, true);
                }
            }
            else if (_isMovingRandomly)
                _randomMovementLogic.MoveRandomly();
            else
                MoveTo(GetDirectionToPlayer());
        }
    }
}