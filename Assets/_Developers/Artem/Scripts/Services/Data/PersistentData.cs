namespace MythicalBattles
{
    public class PersistentData : IPersistentData
    {
        public PlayerData PlayerData { get; set; }
        public GameProgressData GameProgressData { get; set; }
        public SettingsData SettingsData { get; set; }
    }
}
