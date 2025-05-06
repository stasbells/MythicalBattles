namespace MythicalBattles
{
    public class AncientWarriorMover : RangeEnemyMover
    {
        protected override void Attack()
        {
            RotateTowards(GetDirectionToPlayer());
            
            base.Attack();
        }
    }
}