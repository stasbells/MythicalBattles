using System;
using Newtonsoft.Json;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Services.Data
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class SettingsData
    {
        private const float InitVolume = 0.7f;

        [SerializeField] private float _musicVolume = InitVolume;
        [SerializeField] private float _soundsVolume = InitVolume;

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
