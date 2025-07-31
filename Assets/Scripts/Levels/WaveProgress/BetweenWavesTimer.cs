using System;
using MythicalBattles.Assets.Scripts.Utils;
using R3;
using TMPro;

namespace MythicalBattles.Assets.Scripts.Levels.WaveProgress
{
    public class BetweenWavesTimer
    {
        private const string NextWaveTextFormat = "Next wave in";
        
        private readonly TMP_Text _timerText;
        private readonly int _timeBetweenWaves;
        private IDisposable _timerSubscription;

        public BetweenWavesTimer(TMP_Text timerText, int timeBetweenWaves)
        {
            _timerText = timerText;
            _timeBetweenWaves = timeBetweenWaves;
        }

        public event Action Elapsed;

        public void Start()
        {
            _timerSubscription?.Dispose();
            
            var ticks = _timeBetweenWaves - 1;
            
            _timerText.text = $"{LanguagesDictionary.GetTranslation(NextWaveTextFormat)} {ticks}";
            
            _timerSubscription = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => UpdateTimer(ref ticks));
        }

        private void UpdateTimer(ref int ticks)
        {
            _timerText.text = $"{LanguagesDictionary.GetTranslation(NextWaveTextFormat)} {--ticks}";

            if (ticks <= 0)
            {
                _timerSubscription?.Dispose();
                
                Elapsed?.Invoke();
            }
        }
    }
}