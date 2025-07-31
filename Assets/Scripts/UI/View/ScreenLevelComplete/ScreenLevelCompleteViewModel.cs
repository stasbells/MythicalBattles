using MythicalBattles.Assets.Scripts.UI.View.ScreenGameplay;
using R3;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenLevelComplete
{
    public class ScreenLevelCompleteViewModel : ScreenViewModel
    {
        private readonly Subject<Unit> _exitSceneRequest;
        private readonly Subject<Unit> _restartSceneRequest;
        private readonly GameplayUIManager _uiManager;

        public ScreenLevelCompleteViewModel(GameplayUIManager uiManager, float levelPassTime, float bestTime, int score, int rewardMoney,
            Subject<Unit> exitSceneRequest, Subject<Unit> restartSceneRequest)
        {
            LevelPassTime = levelPassTime;
            BestTime = bestTime;
            Score = score;
            RewardMoney = rewardMoney;
            _exitSceneRequest = exitSceneRequest;
            _restartSceneRequest = restartSceneRequest;
            _uiManager = uiManager;
        }

        public float LevelPassTime { get; }
        public float BestTime { get; }
        public int Score { get; }
        public int RewardMoney { get; }
        public override string Name => "ScreenLevelComplete";

        public void RequestOpenScreenGameComplete()
        {
            _uiManager.OpenScreenGameComplete();
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