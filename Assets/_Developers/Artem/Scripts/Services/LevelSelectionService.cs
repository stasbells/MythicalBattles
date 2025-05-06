namespace MythicalBattles
{
    public class LevelSelectionService : ILevelSelectionService
    {
        public LevelSelectionService()
        {
            CurrentLevelNumber = 1; //потом сделать так чтобы он задавался перед запуском сцены
        }
        
        public int CurrentLevelNumber { get; private set; }

        public void SelectLevel(int levelIndex)
        {
            CurrentLevelNumber = levelIndex;
        }
    }
}
