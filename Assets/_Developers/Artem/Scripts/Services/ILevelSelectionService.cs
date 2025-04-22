namespace MythicalBattles
{
    public interface ILevelSelectionService
    {
        public int CurrentLevel { get; }
        public void SelectLevel(int levelIndex);
    }
}
