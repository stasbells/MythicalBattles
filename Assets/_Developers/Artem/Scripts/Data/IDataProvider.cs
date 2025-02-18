namespace MythicalBattles
{
    public interface IDataProvider
    {
        void Save();
        bool TryLoad();
    }
}
