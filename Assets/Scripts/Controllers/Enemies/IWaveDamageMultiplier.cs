namespace MythicalBattles.Assets.Scripts.Controllers.Enemies
{
    public interface IWaveDamageMultiplier
    {
        public void ApplyMultiplier(float multiplier);
        
        public void CancelMultiplier();
    }
}
