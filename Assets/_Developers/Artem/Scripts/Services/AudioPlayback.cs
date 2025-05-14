using Ami.BroAudio;
using UnityEngine;

namespace MythicalBattles
{
    public class AudioPlayback : IAudioPlayback
    {
        private IPersistentData _persistentData;
        
        public AudioPlayback(IPersistentData persistentData)
        {
            _persistentData = persistentData;
            AudioContainer = Resources.Load<AudioContainer>("Prefabs/AudioContainer");
        }
        
        public AudioContainer AudioContainer { get;  set; }
        
        public void Play(SoundID soundID)
        {
            AudioContainer.Play(soundID, _persistentData.SettingsData.Volume);
        }
        
        public void StopPlay(SoundID soundID)
        {
            AudioContainer.Stop(soundID);
        }

        public void ChangeVolume(float volume)
        {
           _persistentData.SettingsData.SetVolume(volume);
           
           AudioContainer.SetVolume(volume);
        }
    }
}
