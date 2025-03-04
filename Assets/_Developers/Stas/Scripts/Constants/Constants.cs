using UnityEngine;

namespace MythicalBattles
{
    public static class Constants
    {
        public static readonly int IsMove = Animator.StringToHash("isMove");
        public static readonly int IsShoot = Animator.StringToHash("isShoot");
        public static readonly int IsAim = Animator.StringToHash("isAim");
        public static readonly int IsDead = Animator.StringToHash("isDead");
        public static readonly int IsAttack = Animator.StringToHash("isAttack");
    }
}