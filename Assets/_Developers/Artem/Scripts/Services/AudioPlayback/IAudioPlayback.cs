using Ami.BroAudio;
using MythicalBattles.Audio;

namespace MythicalBattles.Services.AudioPlayback
{
    public interface IAudioPlayback
    {
        public AudioContainer AudioContainer { get; set; }
        public void PlayMusic(SoundID soundID);
        public void PlaySound(SoundID soundID);
        public void PlayLevelThemeAfterBossTheme();
        public void StopPlay(SoundID soundID);
        public void ChangeMusicVolume(float volume);
        public void ChangeSoundsVolume(float volume);
    }
}
