namespace MythicalBattles.Assets.Scripts.UI.View.ScreenGameplay
{
    public class ScreenGameplayViewModel : ScreenViewModel
    {
        private readonly GameplayUIManager _uiManager;

        public override string Name => "ScreenGameplay";

        public ScreenGameplayViewModel(GameplayUIManager uIManager)
        {
            _uiManager = uIManager;
        }

        public void RequestGoToPopupPause()
        {
            _uiManager.OpenPopupPause();
        }
    }
}