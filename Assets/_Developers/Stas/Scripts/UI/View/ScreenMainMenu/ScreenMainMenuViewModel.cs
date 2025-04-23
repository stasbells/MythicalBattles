using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu;
using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu
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

        public void RequestGoToSceneGameplay()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }

        public void RequestGoToScreenMainMenu()
        {
            _uiManager.OpenScreenMainMenu();
        }

        internal void RequestGoToScreenSettings()
        {
            _uiManager.OpenScreenSettings();
        }

        internal void RequestGoToScreenShop()
        {
            _uiManager.OpenScreenShop();
        }

        internal void RequestGoToScreenLeaderboard()
        {
            _uiManager.OpenScreenLeaderboard();
        }
    }
}