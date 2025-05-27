using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;

namespace MythicalBattles
{
    public class SkeletonMover : MeleeEnemyMover
    {
        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            
            if (Animator.GetBool(Constants.IsDead))
                return;
            
            if (GetDistanceToPlayer() <= AttackDistance)
                Animator.SetBool(Constants.IsAttack, true);
            else
                MoveTo(GetDirectionToPlayer());
        }
    }
}