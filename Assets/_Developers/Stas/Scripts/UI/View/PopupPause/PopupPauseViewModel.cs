using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupA
{
    public class PopupPauseViewModel : ScreenViewModel
    {
        private readonly Subject<Unit> _exitSceneRequest;

        public override string Name => "PopupPause";

        public PopupPauseViewModel(Subject<Unit> exitSceneRequest)
        {
            _exitSceneRequest = exitSceneRequest;
        }

        public void RequestGoToMainMenu()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}