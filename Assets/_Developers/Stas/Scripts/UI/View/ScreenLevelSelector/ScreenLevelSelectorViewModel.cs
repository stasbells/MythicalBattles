using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu;
using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelSelector
{
    public class ScreenLevelSelectorViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;
        private readonly Subject<Unit> _exitSceneRequest;

        public ScreenLevelSelectorViewModel(MainMenuUIManager mainMenuUIManager, Subject<Unit> exitSceneRequest)
        {
            _uiManager = mainMenuUIManager;
            _exitSceneRequest = exitSceneRequest;
        }

        public override string Name => "ScreenLevelSelector";

        public void RequestGoToSceneGameplay()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }

        public void RequestGoToScreenMainMenu()
        {
            _uiManager.OpenScreenMainMenu();
        }

        public void RequestGoToTutorial()
        {
            _uiManager.OpenScreenTutorial();
        }
    }
}