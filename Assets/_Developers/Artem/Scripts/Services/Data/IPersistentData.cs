namespace MythicalBattles
{
    public interface IPersistentData
    {
        PlayerData PlayerData { get; set; }
        GameProgressData GameProgressData { get; set; }
    }
}
