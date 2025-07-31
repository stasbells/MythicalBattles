namespace MythicalBattles.Assets.Scripts.Controllers.Enemies.Movers
{
    public class AncientWarriorMover : RangeEnemyMover
    {
        protected override void OnRangeEnemyMoverAttack()
        {
            RotateTowards(GetDirectionToPlayer());
        }
    }
}