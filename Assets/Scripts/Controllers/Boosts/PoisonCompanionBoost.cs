namespace MythicalBattles.Assets.Scripts.Controllers.Boosts
{
    public class PoisonCompanionBoost : CompanionBoost
    {
        protected override void Apply()
        {
            base.Apply();
            
            CompanionSpawner.SpawnPoisonCompanion();
        }
    }
}
