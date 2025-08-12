
namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public bool IsFirstSession = true;

        public int IdSave;

        public string JsonPlayerData;
        public string JsonGameProgressData;
        public string JsonGameSettingsData;
    }
}