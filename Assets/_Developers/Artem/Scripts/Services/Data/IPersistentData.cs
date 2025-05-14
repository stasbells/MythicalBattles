namespace MythicalBattles
{
    public interface IPersistentData
    {
        public PlayerData PlayerData { get; set; }
        public GameProgressData GameProgressData { get; set; }
        public SettingsData SettingsData { get; set; }
        
    }
}
