using System;
using MythicalBattles.Assets.Scripts.Utils;
using R3;
using TMPro;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Levels.WaveProgress
{
    public class BetweenWavesTimer
    {
        private IDisposable _timerSubscription;
        private int _timeBetweenWaves;
        private int _ticks;

        public BetweenWavesTimer(int timeBetweenWaves)
        {
            _timeBetweenWaves = timeBetweenWaves;
        }

        public event Action Elapsed;
        public event Action<int> Ticked;

        public void Start()
        {
            _timerSubscription?.Dispose();
            
            _ticks = _timeBetweenWaves;
            
            _timerSubscription = Observable
                .Interval(TimeSpan.FromSeconds(1f))
                .Subscribe(_ => UpdateTimer());
        }

        private void UpdateTimer()
        {
            _ticks--;
            
            Ticked?.Invoke(_ticks);
            
            if (_ticks == 0)
            {
                _timerSubscription?.Dispose();
                
                Elapsed?.Invoke();
            }
        }
    }
}