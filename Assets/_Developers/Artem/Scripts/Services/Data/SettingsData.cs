using System;
using Newtonsoft.Json;

namespace MythicalBattles
{
    [Serializable]
    public class SettingsData
    {
        private const float InitVolume = 0.7f;

        public SettingsData()
        {
            MusicVolume = InitVolume;
            SoundsVolume = InitVolume;
        }

        [JsonConstructor]
        public SettingsData(float musicVolume, float soundsVolume)
        {
            SetMusicVolume(musicVolume);
            
            SetSoundsVolume(soundsVolume);
        }
        
        public float MusicVolume { get; private set; }
        public float SoundsVolume { get; private set; }
        
        public void SetMusicVolume(float volume)
        {
            if(volume < 0 || volume > 1)
                throw new InvalidOperationException();

            MusicVolume = volume;
        }
        
        public void SetSoundsVolume(float volume)
        {
            if(volume < 0 || volume > 1)
                throw new InvalidOperationException();

            SoundsVolume = volume;
        }
    }
}
