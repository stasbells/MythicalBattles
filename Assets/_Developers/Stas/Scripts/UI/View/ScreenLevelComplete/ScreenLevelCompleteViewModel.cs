using System.Collections;
using System.Collections.Generic;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;
using R3;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelComplete
{
    public class ScreenLevelCompleteViewModel : ScreenViewModel
    {
        private readonly Subject<Unit> _exitSceneRequest;
        private readonly Subject<Unit> _restartSceneRequest;

        public float LevelPassTime { get; }
        public float BestTime { get; }
        public int Score { get; }
        public int RewardMoney { get; }

        public override string Name => "ScreenLevelComplete";
        
        public ScreenLevelCompleteViewModel(float levelPassTime, float bestTime, int score, int rewardMoney)
        {
            LevelPassTime = levelPassTime;
            BestTime = bestTime;
            Score = score;
            RewardMoney = rewardMoney;
        }
        
        public void RequestGoToMainMenu()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
        
        public void RequestToRestartLevel()
        {
            _restartSceneRequest.OnNext(Unit.Default);
        }
    }
}
