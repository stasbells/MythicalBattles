using MythicalBattles.Assets.Scripts.UI.View.ScreenMainMenu;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenSettings
{
    public class ScreenSettingsViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;

        public override string Name => "ScreenSettings";

        public ScreenSettingsViewModel(MainMenuUIManager mainMenuUIManager)
        {
            _uiManager = mainMenuUIManager;
        }

        public void RequestGoToScreenMainMenu()
        {
            _uiManager.OpenScreenMainMenu();
        }
    }
}