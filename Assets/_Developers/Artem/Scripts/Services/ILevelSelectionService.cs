namespace MythicalBattles
{
    public interface ILevelSelectionService
    {
        public int CurrentLevelNumber { get; }
        public void SelectLevel(int levelIndex);
    }
}
