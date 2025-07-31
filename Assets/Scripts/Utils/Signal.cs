using R3;

namespace MythicalBattles.Assets.Scripts.Utils
{
    public class Signal
    {
        private readonly Subject<Unit> _exitSceneRequest = new();
        private readonly Subject<Unit> _restartSceneRequest = new();

        public Subject<Unit> ExitSceneRequest => _exitSceneRequest;
        public Subject<Unit> RestartSceneRequest => _restartSceneRequest;
    }
}