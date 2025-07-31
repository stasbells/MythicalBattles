using Ami.BroAudio;
using MythicalBattles.Assets.Scripts.Audio;
using MythicalBattles.Assets.Scripts.Services.Data;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Services.AudioPlayback
{
    public class AudioPlayback : IAudioPlayback
    {
        private const string AudioContainerPrefabPath = "Prefabs/AudioContainer";
        
        private float _currentMusicVolume;
        private float _currentSoundsVolume;
        private SoundID CurrentLevelThemeID;
        
        private IPersistentData _persistentData;
        private IDataProvider _dataProvider;

        public AudioPlayback(IPersistentData persistentData, IDataProvider dataProvider)
        {
            _persistentData = persistentData;
            _dataProvider = dataProvider;
            
            AudioContainer = Resources.Load<AudioContainer>(AudioContainerPrefabPath);
        }

        public AudioContainer AudioContainer { get;  set; }

        public void PlayMusic(SoundID soundID)
        {
            _currentMusicVolume = _persistentData.SettingsData.MusicVolume;         
            
            AudioContainer.PlayMusic(soundID, _currentMusicVolume);
        }
        
        public void PlaySound(SoundID soundID)
        {
            _currentSoundsVolume = _persistentData.SettingsData.SoundsVolume;         

            AudioContainer.PlaySound(soundID, _currentSoundsVolume);
        }
        
        public void PlayLevelThemeAfterBossTheme()
        {
            _currentMusicVolume = _persistentData.SettingsData.MusicVolume;

            AudioContainer.PlayLevelThemeAfterBossTheme(_currentMusicVolume);
        }
        
        public void ChangeMusicVolume(float volume)
        {
            _currentMusicVolume = volume;
            
           _persistentData.SettingsData.SetMusicVolume(volume);

           AudioContainer.SetMusicVolume(volume);
           
           _dataProvider.SaveSettingsData();
        }
        
        public void ChangeSoundsVolume(float volume)
        {
            _currentSoundsVolume = volume;
            
            _persistentData.SettingsData.SetSoundsVolume(volume);
            
            _dataProvider.SaveSettingsData();
        }
    }
}
