namespace MythicalBattles
{
    public class LevelSelectionService : ILevelSelectionService
    {
        private const int LastLevelIndex = 8;

        public int CurrentLevelNumber { get; private set; }
        public int LastLevelNumber => LastLevelIndex + 1;

        public void SelectLevel(int levelIndex)
        {
            CurrentLevelNumber = levelIndex;
        }
    }
}
