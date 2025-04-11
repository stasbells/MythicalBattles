namespace MythicalBattles
{
    public interface IDamageDealComponent
    {
        public void ApplyWaveDamageMultiplier(float multiplier);
        
        public void CancelWaveDamageMultiplier();
    }
}
