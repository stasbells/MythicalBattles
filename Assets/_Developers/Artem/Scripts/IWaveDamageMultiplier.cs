namespace MythicalBattles
{
    public interface IWaveDamageMultiplier
    {
        public void ApplyMultiplier(float multiplier);
        
        public void CancelMultiplier();
    }
}
