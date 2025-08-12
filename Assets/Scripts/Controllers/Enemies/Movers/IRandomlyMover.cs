using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies.Movers
{
    public interface IRandomlyMover
    {
        public void MoveTo(Vector3 randomdirection);
        public void StopRandomMoving();
    }
}