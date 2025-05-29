namespace MythicalBattles
{
    public class LevelSelectionService : ILevelSelectionService
    {
        private const int LastLevelIndex = 1;
        
        public LevelSelectionService()
        {
            CurrentLevelNumber = 1; //потом удалить конструктор и сделать так чтобы номер задавался перед запуском сцены
        }
        
        public int CurrentLevelNumber { get; private set; }

        public int LastLevelNumber => LastLevelIndex;

        public void SelectLevel(int levelIndex)
        {
            CurrentLevelNumber = levelIndex;
        }
    }
}
