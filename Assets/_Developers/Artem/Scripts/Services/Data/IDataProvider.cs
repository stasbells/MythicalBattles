using System;

namespace MythicalBattles
{
    public interface IDataProvider
    {
        public event Action PlayerDataReseted;
        public void SavePlayerData();
        public void SaveGameProgressData();
        public bool TryLoadPlayerData();
        public bool TryLoadGameProgressData();
        public void ResetPlayerData();
    }
}
