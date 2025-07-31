namespace MythicalBattles.Boosts
{
    public class FireCompanionBoost : CompanionBoost
    {
        protected override void Apply()
        {
            base.Apply();
            
            CompanionSpawner.SpawnFireCompanion();
        }
    }
}
