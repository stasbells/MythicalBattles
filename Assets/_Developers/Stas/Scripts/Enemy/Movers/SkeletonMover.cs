namespace MythicalBattles
{
    public class SkeletonMover : MeleeEnemyMover
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            if (Animator.GetBool(Constants.IsDead))
                return;
            
            if (GetDistanceToPlayer() <= AttackDistance)
                Animator.SetBool(Constants.IsAttack, true);
            else
                MoveTo(GetDirectionToPlayer());
        }
    }
}