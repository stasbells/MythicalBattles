namespace MythicalBattles
{
    public class ElectricCompanionBoost : CompanionBoost
    {
        protected override void Apply()
        {
            base.Apply();
            
            CompanionSpawner.SpawnElectricCompanion();
        }
    }
}
