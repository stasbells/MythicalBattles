
namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public bool isFirstSession = true;

        public int idSave;

        public string JsonPlayerData;
        public string JsonGameProgressData;
        public string JsonGameSettingsData;
    }
}