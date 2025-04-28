using UnityEngine;

namespace MythicalBattles
{
    public class DemonMover : MeleeEnemyMover, IRandomlyMover
    {
        private const float BaseMoveSpeed = 3f;

        [SerializeField] private float _playerFollowTime = 4f;
        [SerializeField] private float _durationOfRandomMove = 2f;
        [SerializeField] private float _directionChangeInterval = 1f;
        [SerializeField] private float _raycastDistance = 7f;
        [SerializeField] private float _attackDuration = 1f;
        [SerializeField] ParticleSystem _effect;

        private RandomMovementLogic _randomMovementLogic;
        private float _playerFollowTimer;
        private float _moveAnimationSpeedMultiplier;
        private float _attackTimer;
        private bool _isMovingRandomly;

        protected override void OnAwake()
        {
            base.OnAwake();

            _effect.Stop();

            _randomMovementLogic = new RandomMovementLogic(this, Transform, _durationOfRandomMove,
                _directionChangeInterval,
                _raycastDistance);
        }

        protected override void OnStart()
        {
            base.OnStart();

            CorrectMoveAnimationSpeed();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (Animator.GetBool(Constants.IsDead))
                return;

            if (Animator.GetBool(Constants.IsAttack) || Animator.GetBool(Constants.IsMeleeAttack))
                return;

            if (GetDistanceToPlayer() <= AttackDistance && _isMovingRandomly == false)
            {
                AttackInMelee();
                _playerFollowTimer = 0f;
            }
            else if (_isMovingRandomly)
            {
                _randomMovementLogic.MoveRandomly();
            }
            else
            {
                _effect.Stop();

                MoveTo(GetDirectionToPlayer());

                _playerFollowTimer += Time.deltaTime;

                if (_playerFollowTimer >= _playerFollowTime)
                {
                    _playerFollowTimer = 0f;
                    CastSpell();
                }
            }
        }

        protected override void OnEnableBehaviour()
        {
            base.OnEnableBehaviour();

            Animator.SetBool(Constants.IsMove, true);
        }

        private void CorrectMoveAnimationSpeed()
        {
            _moveAnimationSpeedMultiplier = MoveSpeed / BaseMoveSpeed;
            Animator.SetFloat(Constants.MoveSpeed, _moveAnimationSpeedMultiplier);
        }

        public void StopRandomMoveAndCastSpell()
        {
            _playerFollowTimer = 0f;
            CastSpell();
        }

        public void AttackEffect()
        {
            _effect.Play();
        }

        public void StopRandomMoving()
        {
            _isMovingRandomly = false;
        }

        private void AttackInMelee()
        {
            Animator.SetBool(Constants.IsMeleeAttack, true);
            Animator.SetBool(Constants.IsMove, false);
        }

        private void CastSpell()
        {
            Animator.SetBool(Constants.IsAttack, true);
            Animator.SetBool(Constants.IsMove, false);

            if(Mathf.Approximately(_durationOfRandomMove, 0f) == false)
                _isMovingRandomly = !_isMovingRandomly;
            
            _randomMovementLogic.ResetMoveTimer();
        }
    }
}