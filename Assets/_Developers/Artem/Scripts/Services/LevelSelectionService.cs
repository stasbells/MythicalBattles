namespace MythicalBattles
{
    public class LevelSelectionService : ILevelSelectionService
    {
        public int CurrentLevel { get; private set; }

        public void SelectLevel(int levelIndex)
        {
            CurrentLevel = levelIndex;
        }
    }
}
