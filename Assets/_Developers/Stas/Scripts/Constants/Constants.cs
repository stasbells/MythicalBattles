using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace MythicalBattles
{
    public static class Constants
    {
        public static readonly float MoveControllerRotationAngle = 45f;

        public static readonly float SpawnPointXPlus = 5.7f;
        public static readonly float SpawnPointXMinus = -6.7f;
        public static readonly float SpawnPointZPlus = 4.5f;
        public static readonly float SpawnPointZMinus = -15.7f;
        public static readonly float SpawnPointY = 8.0f;

        public static readonly int LayerDefault = LayerMask.NameToLayer("Default");
        public static readonly int LayerTransparentFX = LayerMask.NameToLayer("TransparentFX");
        public static readonly int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        public static readonly int LayerEnemy = LayerMask.NameToLayer("Enemy");
        public static readonly int LayerWater = LayerMask.NameToLayer("Water");
        public static readonly int LayerUI = LayerMask.NameToLayer("UI");
        public static readonly int LayerObstacles = LayerMask.NameToLayer("Obstacles");
        public static readonly int LayerPlayer = LayerMask.NameToLayer("Player");
        public static readonly int LayerPlayerProjectile = LayerMask.NameToLayer("PlayerProjectile");
        public static readonly int LayerEnemyProjectile = LayerMask.NameToLayer("EnemyProjectile");

        public static readonly int MaskLayerDefault = 1 << LayerDefault;
        public static readonly int MaskLayerTransparentFX = 1 << LayerTransparentFX;
        public static readonly int MaskLayerIgnoreRaycast = 1 << LayerIgnoreRaycast;
        public static readonly int MaskLayerEnemy = 1 << LayerEnemy;
        public static readonly int MaskLayerWater = 1 << LayerWater;
        public static readonly int MaskLayerUI = 1 << LayerUI;
        public static readonly int MaskLayerObstacles = 1 << LayerObstacles;
        public static readonly int MaskLayerPlayer = 1 << LayerPlayer;
        public static readonly int MaskLayerPlayerProjectile = 1 << LayerPlayerProjectile;
        public static readonly int MaskLayerEnemyProjectile = 1 << LayerEnemyProjectile;

        public static readonly int IsMove = Animator.StringToHash("isMove");
        public static readonly int IsDead = Animator.StringToHash("isDead");
        public static readonly int IsAttack = Animator.StringToHash("isAttack");
        public static readonly int IsShoot = Animator.StringToHash("isShoot");
    }
}