using UnityEngine;

namespace MythicalBattles
{
    public class DemonMoveBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(Constants.IsAttack, false);
            animator.SetBool(Constants.IsMeleeAttack, false);
            animator.SetBool(Constants.IsMove, true);
        }
    }
}
