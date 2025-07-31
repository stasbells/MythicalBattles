using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles
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