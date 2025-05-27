using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using UnityEngine;

namespace MythicalBattles
{
    public class RandomMovementLogic
    {
        private IRandomlyMover _mover;
        private float _moveTimer;
        private float _directionChangeTimer;
        private Vector3 _randomDirection;
        private float _durationOfRandomMove;
        private float _raycastDistance;
        private float _directionChangeInterval;
        private Transform _transform;
        
        public RandomMovementLogic(IRandomlyMover mover, Transform transform, float durationOfRandomMove, float directionChangeInterval, float raycastDistance)
        {
            _mover = mover;
            _transform = transform;
            _durationOfRandomMove = durationOfRandomMove;
            _directionChangeInterval = directionChangeInterval;
            _raycastDistance = raycastDistance;
            _randomDirection = Vector3.zero;
        }
        
        public void MoveRandomly()
        {
            _moveTimer += Time.deltaTime;
            _directionChangeTimer += Time.deltaTime;

            if (_moveTimer >= _durationOfRandomMove)
            {
                _moveTimer = 0f;

                switch (_mover)
                {
                    case DemonMover demonMover:
                        demonMover.StopRandomMoveAndCastSpell();
                        return;
                    case RangeEnemyMover rangeEnemyMover:
                        rangeEnemyMover.ResetStopTimer();
                        break;
                }
                
                _mover.StopRandomMoving();
            }
            else
            {
                if (_directionChangeTimer >= _directionChangeInterval)
                {
                    _directionChangeTimer = 0f;
                    _randomDirection = GetFreeRandomDirection();
                }

                if(_randomDirection == Vector3.zero)
                    _randomDirection = GetFreeRandomDirection();

                _mover.MoveTo(_randomDirection);
            }
        }

        public void ResetMoveTimer()
        {
            _moveTimer = 0f;
        }
        
        private Vector3 GetFreeRandomDirection()
        {
            Vector3 direction = GetRandomDirection();

            while (TryFindObstacleIn(direction))
                direction = GetRandomDirection();

            return direction;
        }
        
        private Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }
        
        private bool TryFindObstacleIn(Vector3 direction)
        {
            if (Physics.Raycast(_transform.position, direction, out _, _raycastDistance, Constants.MaskLayerObstacles))
            {
                Debug.DrawRay(_transform.position, direction * _raycastDistance, Color.red, 1f);

                return true;
            }

            Debug.DrawRay(_transform.position, direction * _raycastDistance, Color.green, 1f);

            return false;
        }
    }
}
