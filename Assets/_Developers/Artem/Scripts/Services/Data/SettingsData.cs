using System;
using Newtonsoft.Json;

namespace MythicalBattles
{
    [Serializable]
    public class SettingsData
    {
        private const float InitVolume = 1f;

        public SettingsData()
        {
            Volume = InitVolume;
        }

        [JsonConstructor]
        public SettingsData(float volume)
        {
            SetVolume(volume);
        }
        
        public float Volume { get; private set; }

        public void SetVolume(float volume)
        {
            if(volume < 0 || volume > 1)
                throw new InvalidOperationException();

            Volume = volume;
        }
    }
}
