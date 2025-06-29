using System;

namespace MythicalBattles
{
    public interface IDataProvider
    {
        public event Action PlayerDataReseted;
        public void SavePlayerData();
        public void SaveGameProgressData();
        public void SaveSettingsData();
        public void LoadPlayerData();
        public void LoadGameProgressData();
        public void LoadSettingsData();
        public void ResetPlayerData();
        public void ResetProgressData();
    }
}
