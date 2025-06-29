using System;
using Newtonsoft.Json;
using UnityEngine;

namespace MythicalBattles
{
    [Serializable]
    public class SettingsData
    {
        private const float InitVolume = 0.7f;

        [SerializeField] private float _musicVolume = InitVolume;
        [SerializeField] private float _soundsVolume = InitVolume;

        // public SettingsData()
        // {
        //     MusicVolume = InitVolume;
        //     SoundsVolume = InitVolume;
        // }
        //
        // [JsonConstructor]
        // public SettingsData(float musicVolume, float soundsVolume)
        // {
        //     SetMusicVolume(musicVolume);
        //     
        //     SetSoundsVolume(soundsVolume);
        // }

        public float MusicVolume => _musicVolume;
        public float SoundsVolume => _soundsVolume;
        
        public void SetMusicVolume(float volume)
        {
            if(volume < 0 || volume > 1)
                throw new InvalidOperationException();

            _musicVolume = volume;
        }
        
        public void SetSoundsVolume(float volume)
        {
            if(volume < 0 || volume > 1)
                throw new InvalidOperationException();

            _soundsVolume = volume;
        }
    }
}
