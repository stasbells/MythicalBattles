namespace MythicalBattles.Assets.Scripts.Controllers.Boosts
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
