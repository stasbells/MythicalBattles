using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay
{
    public class ScreenGameplayViewModel : ScreenViewModel 
    {
        private readonly GameplayUIManager _uIManager;
        private readonly Subject<Unit> _exitSceneRequest;

        public override string Name => "ScreenGameplay";

        public ScreenGameplayViewModel(GameplayUIManager uIManager, Subject<Unit> exitSceneRequest)
        {
            _uIManager = uIManager;
            _exitSceneRequest = exitSceneRequest;
        }

        public void RequestGoToPopupA()
        {
            _uIManager.OpenPopupA();
        }

        public void RequestGoToPopupB()
        {
            _uIManager.OpenPopupB();
        }

        public void RequestGoToMainMenu()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}