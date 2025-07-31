namespace MythicalBattles
{
    public class AncientWarriorMover : RangeEnemyMover
    {
        protected override void OnRangeEnemyMoverAttack()
        {
            RotateTowards(GetDirectionToPlayer());
        }
    }
}