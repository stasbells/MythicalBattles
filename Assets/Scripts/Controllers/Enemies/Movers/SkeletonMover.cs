using MythicalBattles.Assets.Scripts.Utils;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies.Movers
{
    public class SkeletonMover : MeleeEnemyMover
    {
        protected override void OnMeleeEnemyMoverFixedUpdate()
        {
            if (Animator.GetBool(Constants.IsDead))
                return;
            
            if (GetDistanceToPlayer() <= AttackDistance)
                Animator.SetBool(Constants.IsAttack, true);
            else
                MoveTo(GetDirectionToPlayer());
        }
    }
}