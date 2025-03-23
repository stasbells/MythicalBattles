namespace MythicalBattles
{
    public class ElectricCompanionBoost : CompanionBoost
    {
        protected override void Apply()
        {
            CompanionSpawner.SpawnElectricCompanion();
        }
    }
}
