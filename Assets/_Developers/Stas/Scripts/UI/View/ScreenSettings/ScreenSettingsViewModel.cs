using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenSettings
{
    public class ScreenSettingsViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;

        public override string Name => "ScreenSettings";

        public ScreenSettingsViewModel(MainMenuUIManager mainMenuUIManager)
        {
            _uiManager = mainMenuUIManager;
        }

        internal void RequestGoToScreenMainMenu()
        {
            _uiManager.OpenScreenMainMenu();
        }
    }
}