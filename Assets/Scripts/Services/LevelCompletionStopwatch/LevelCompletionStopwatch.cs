using System;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Services.LevelCompletionStopwatch
{
    public class LevelCompletionStopwatch : ILevelCompletionStopwatch
    {
        private float _startTime;
        private float _elapsedTime;
        private bool _isRunning;

        public float ElapsedTime => _isRunning ? Time.time - _startTime : _elapsedTime;

        public void Start()
        {
            if (_isRunning)
                throw new InvalidOperationException();

            _startTime = Time.time - _elapsedTime;
            _isRunning = true;
        }

        public void Stop()
        {
            if (_isRunning == false)
                throw new InvalidOperationException();

            _elapsedTime = Time.time - _startTime;
            _isRunning = false;
        }

        public void Reset()
        {
            _elapsedTime = 0f;
            _isRunning = false;
        }
    }
}