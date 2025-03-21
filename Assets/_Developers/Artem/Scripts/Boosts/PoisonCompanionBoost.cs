namespace MythicalBattles
{
    public class PoisonCompanionBoost : CompanionBoost
    {
        protected override void Apply()
        {
            CompanionSpawner.SpawnPoisonCompanion();
        }
    }
}
