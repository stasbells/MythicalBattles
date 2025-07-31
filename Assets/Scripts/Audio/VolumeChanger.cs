using MythicalBattles.Services.AudioPlayback;
using MythicalBattles.Services.Data;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Audio
{
    public class VolumeChanger : MonoBehaviour
    {
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _soundsVolumeSlider;
        
        private IPersistentData _persistentData;
        private IAudioPlayback _audioPlayback;
        
        private void Construct()
        {
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }

        private void Awake()
        {
            Construct();

            _musicVolumeSlider.value = _persistentData.SettingsData.MusicVolume;
            _soundsVolumeSlider.value = _persistentData.SettingsData.SoundsVolume;
        }
        
        public void ChangeMusicVolume()
        {
            _audioPlayback.ChangeMusicVolume(_musicVolumeSlider.value);
        }

        public void ChangeSoundsVolume()
        {
            _audioPlayback.ChangeSoundsVolume(_soundsVolumeSlider.value);
        }
    }
}
