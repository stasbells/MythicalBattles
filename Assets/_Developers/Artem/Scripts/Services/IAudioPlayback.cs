using Ami.BroAudio;

namespace MythicalBattles
{
    public interface IAudioPlayback
    {
        public AudioContainer AudioContainer { get; set; }
        public void Play(SoundID soundID);
        public void StopPlay(SoundID soundID);
        public void ChangeVolume(float volume);
    }
}
