using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenDeath
{
    public class ScreenDeathViewModel : ScreenViewModel
    {
        private readonly Subject<Unit> _exitSceneRequest;
        private readonly Subject<Unit> _restartSceneRequest;

        public ScreenDeathViewModel(Subject<Unit> exitSceneRequest, Subject<Unit> restartSceneRequest)
        {
            _exitSceneRequest = exitSceneRequest;
            _restartSceneRequest = restartSceneRequest;
        }

        public override string Name => "ScreenDeath";

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
