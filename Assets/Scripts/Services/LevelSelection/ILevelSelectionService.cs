namespace MythicalBattles.Assets.Scripts.Services.LevelSelection
{
    public interface ILevelSelectionService
    {
        public int CurrentLevelNumber { get; }
        public int LastLevelNumber { get; }
        public void SelectLevel(int levelIndex);
    }
}
