namespace MythicalBattles.Assets.Scripts.Controllers.Boosts
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
