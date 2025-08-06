namespace MythicalBattles.Assets.Scripts.UI.View.ScreenGameplay
{
    public class ScreenGameplayViewModel : ScreenViewModel
    {
        private readonly GameplayUIManager _uiManager;

        public ScreenGameplayViewModel(GameplayUIManager uIManager)
        {
            _uiManager = uIManager;
        }
        
        public override string Name => "ScreenGameplay";

        public void RequestGoToPopupPause()
        {
            _uiManager.OpenPopupPause();
        }
    }
}