using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;

namespace MythicalBattles
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