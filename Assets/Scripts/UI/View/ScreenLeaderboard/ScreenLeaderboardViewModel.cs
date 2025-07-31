using MythicalBattles.Assets.Scripts.UI.View.ScreenMainMenu;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenLeaderboard
{
    public class ScreenLeaderboardViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;

        public override string Name => "ScreenLeaderboard";

        public ScreenLeaderboardViewModel(MainMenuUIManager mainMenuUIManager)
        {
            _uiManager = mainMenuUIManager;
        }

        public void RequestGoToScreenMainMenu()
        {
            _uiManager.OpenScreenMainMenu();
        }
    }
}