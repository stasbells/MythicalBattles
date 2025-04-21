using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu
{
    public class ScreenMainMenuViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;
        private readonly Subject<Unit> _exitSceneRequest;

        public override string Name => "ScreenMainMenu";

        public ScreenMainMenuViewModel(MainMenuUIManager mainMenuUIManager, Subject<Unit> exitSceneRequest)
        {
            _uiManager = mainMenuUIManager;
            _exitSceneRequest = exitSceneRequest;
        }

        public void RequestGoToGameplay()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}