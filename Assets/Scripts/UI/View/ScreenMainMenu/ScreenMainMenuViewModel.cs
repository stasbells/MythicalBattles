namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu
{
    public class ScreenMainMenuViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;

        public override string Name => "ScreenMainMenu";

        public ScreenMainMenuViewModel(MainMenuUIManager mainMenuUIManager)
        {
            _uiManager = mainMenuUIManager;
        }

        public void RequestGoToScreenLevelSelector()
        {
            _uiManager.OpenScreenLevelSelector();
        }

        public void RequestGoToScreenSettings()
        {
            _uiManager.OpenScreenSettings();
        }

        public void RequestGoToScreenShop()
        {
            _uiManager.OpenScreenShop();
        }

        public void RequestGoToScreenLeaderboard()
        {
            _uiManager.OpenScreenLeaderboard();
        }
    }
}