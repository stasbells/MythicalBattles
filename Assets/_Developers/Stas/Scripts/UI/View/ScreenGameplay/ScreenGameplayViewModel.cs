namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay
{
    public class ScreenGameplayViewModel : ScreenViewModel 
    {
        private readonly GameplayUIManager _uIManager;

        public override string Name => "ScreenGameplay";

        public ScreenGameplayViewModel(GameplayUIManager uIManager)
        {
            _uIManager = uIManager;

        }

        public void RequestGoToMenu()
        {

        }
    }
}
