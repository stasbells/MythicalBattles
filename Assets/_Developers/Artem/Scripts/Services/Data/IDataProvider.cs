using System;

namespace MythicalBattles
{
    public interface IDataProvider
    {
        public event Action DataReseted;
        void Save();
        bool TryLoad();
        bool IsDataAlreadyExist();
        void ResetData();
    }
}
