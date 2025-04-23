using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLeaderboard
{
    public class ScreenLeaderboardViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;

        public override string Name => "ScreenLeaderboard";

        public ScreenLeaderboardViewModel(MainMenuUIManager mainMenuUIManager)
        {
            _uiManager = mainMenuUIManager;
        }

        internal void RequestGoToScreenMainMenu()
        {
            _uiManager.OpenScreenMainMenu();
        }
    }
}