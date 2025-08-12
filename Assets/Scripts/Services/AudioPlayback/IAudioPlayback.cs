using Ami.BroAudio;
using MythicalBattles.Assets.Scripts.Audio;

namespace MythicalBattles.Assets.Scripts.Services.AudioPlayback
{
    public interface IAudioPlayback
    {
        public AudioContainer AudioContainer { get; set; }
        public void PlayMusic(SoundID soundID);
        public void PlaySound(SoundID soundID);
        public void PlayLevelThemeAfterBossTheme();
        public void ChangeMusicVolume(float volume);
        public void ChangeSoundsVolume(float volume);
    }
}
