using System;
using UnityEngine;

namespace MythicalBattles.Services.LevelCompletionStopwatch
{
    public class LevelCompletionStopwatch : ILevelCompletionStopwatch
    {
        private float _startTime;
        private float _elapsedTime;
        private bool _isRunning;
        
        public float ElapsedTime => _isRunning ? Time.time - _startTime : _elapsedTime;

        public void Start()
        {
            if (_isRunning == false)
            {
                _startTime = Time.time - _elapsedTime;
                _isRunning = true;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Stop()
        {
            if (_isRunning)
            {
                _elapsedTime = Time.time - _startTime;
                _isRunning = false;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Reset()
        {
            _elapsedTime = 0f;
            _isRunning = false;
        }
    }
}
