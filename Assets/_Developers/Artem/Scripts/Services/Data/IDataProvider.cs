using System;

namespace MythicalBattles
{
    public interface IDataProvider
    {
        public event Action PlayerDataReseted;
        public void SavePlayerData();
        public void SaveGameProgressData();
        public void SaveSettingsData();
        public bool TryLoadPlayerData();
        public bool TryLoadGameProgressData();
        public bool TryLoadSettingsData();
        public void ResetPlayerData();
        public void ResetProgressData();
    }
}
