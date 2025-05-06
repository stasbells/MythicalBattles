namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu
{
    public class ScreenShopViewModel : ScreenViewModel
    {
        private readonly MainMenuUIManager _uiManager;

        public override string Name => "ScreenShop";

        public ScreenShopViewModel(MainMenuUIManager mainMenuUIManager)
        {
            _uiManager = mainMenuUIManager;
        }

        internal void RequestGoToScreenMainMenu()
        {
            _uiManager.OpenScreenMainMenu();
        }
    }
}