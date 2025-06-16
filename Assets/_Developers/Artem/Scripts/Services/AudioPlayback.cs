using Ami.BroAudio;
using UnityEngine;

namespace MythicalBattles
{
    public class AudioPlayback : IAudioPlayback
    {
        private float _currentMusicVolume;
        private float _currentSoundsVolume;
        
        private IPersistentData _persistentData;
        private IDataProvider _dataProvider;

        public AudioPlayback(IPersistentData persistentData, IDataProvider dataProvider)
        {
            _persistentData = persistentData;
            _dataProvider = dataProvider;
            
            AudioContainer = Resources.Load<AudioContainer>("Prefabs/AudioContainer");
        }
        
        public AudioContainer AudioContainer { get;  set; }

        public void PlayMusic(SoundID soundID)
        {
            _currentMusicVolume = _persistentData.SettingsData.MusicVolume;
            
            Debug.Log(_currentMusicVolume + "громкость музыки");
            
            AudioContainer.PlayMusic(soundID, _currentMusicVolume);
        }
        
        public void PlaySound(SoundID soundID)
        {
            _currentSoundsVolume = _persistentData.SettingsData.SoundsVolume;
            
            Debug.Log(_currentSoundsVolume + "громкость звука");

            AudioContainer.PlaySound(soundID, _currentSoundsVolume);
        }
        
        public void StopPlay(SoundID soundID)
        {
            AudioContainer.Stop(soundID);
        }
        
        public void ChangeMusicVolume(float volume)
        {
            _currentMusicVolume = volume;
            
           _persistentData.SettingsData.SetMusicVolume(volume);
           
           _dataProvider.SaveSettingsData();
           
           AudioContainer.SetMusicVolume(volume);
        }
        
        public void ChangeSoundsVolume(float volume)
        {
            _currentSoundsVolume = volume;
            
            _persistentData.SettingsData.SetSoundsVolume(volume);
            
            _dataProvider.SaveSettingsData();
        }
    }
}
