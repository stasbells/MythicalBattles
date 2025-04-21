using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay
{
    public class ScreenGameplayViewModel : ScreenViewModel 
    {
        private readonly GameplayUIManager _uiManager;
        private readonly Subject<Unit> _exitSceneRequest;

        public override string Name => "ScreenGameplay";

        public ScreenGameplayViewModel(GameplayUIManager uIManager, Subject<Unit> exitSceneRequest)
        {
            _uiManager = uIManager;
            _exitSceneRequest = exitSceneRequest;
        }

        public void RequestGoToPopupA()
        {
            _uiManager.OpenPopupA();
        }

        public void RequestGoToPopupB()
        {
            _uiManager.OpenPopupB();
        }

        public void RequestGoToMainMenu()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}