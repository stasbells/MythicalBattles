using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies
{
    public class DemonMoveStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(Constants.IsMeleeAttack, false);
            animator.SetBool(Constants.IsAttack, false);
            animator.SetBool(Constants.IsMove, true);
        }
    }
}