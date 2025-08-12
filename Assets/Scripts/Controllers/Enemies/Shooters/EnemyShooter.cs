namespace MythicalBattles.Assets.Scripts.Controllers.Enemies.Shooters
{
    public class EnemyShooter : SimpleShooter, IWaveDamageMultiplier
    {
        public void ApplyMultiplier(float multiplier)
        {
            SetProjectileDamage(Damage * multiplier);
        }

        public void CancelMultiplier()
        {
            SetProjectileDamage(Damage);
        }
    }
}