using R3;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenTutorial
{
    public class ScreenTutorialViewModel : ScreenViewModel
    {
        private readonly Subject<Unit> _exitSceneRequest;

        public ScreenTutorialViewModel(Subject<Unit> exitSceneRequest)
        {
            _exitSceneRequest = exitSceneRequest;
        }

        public override string Name => "ScreenTutorial";

        public void RequestGoToSceneGameplay()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}