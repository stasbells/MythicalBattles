namespace MythicalBattles
{
    public class FireCompanionBoost : CompanionBoost
    {
        protected override void Apply()
        {
            CompanionSpawner.SpawnFireCompanion();
        }
    }
}
