using UnityEngine;

namespace MythicalBattles
{
    public interface IRandomlyMover
    {
        public void MoveTo(Vector3 randomdirection);
        public void StopRandomMoving();
    }
}