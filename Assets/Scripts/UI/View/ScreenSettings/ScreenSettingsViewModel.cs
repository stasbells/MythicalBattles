using MythicalBattles.Assets.Scripts.UI.View.ScreenMainMenu;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenSettings
{
    public class ScreenSettingsViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;
        
        public ScreenSettingsViewModel(MainMenuUIManager mainMenuUIManager)
        {
            _uiManager = mainMenuUIManager;
        }
        
        public override string Name => "ScreenSettings";

        public void RequestGoToScreenMainMenu()
        {
            _uiManager.OpenScreenMainMenu();
        }
    }
}