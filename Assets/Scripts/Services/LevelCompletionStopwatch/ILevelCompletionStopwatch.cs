namespace MythicalBattles.Assets.Scripts.Services.LevelCompletionStopwatch
{
    public interface ILevelCompletionStopwatch
    {
        public float ElapsedTime { get; }

        public void Start();
        
        public void Stop();
        
        public void Reset();
    }
}
