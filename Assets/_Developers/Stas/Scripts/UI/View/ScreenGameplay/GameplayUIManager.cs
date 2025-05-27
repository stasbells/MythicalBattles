using MythicalBattles.Assets._Developers.Stas.Scripts.UI.Root.Gameplay;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupPause;
using R3;
using Reflex.Core;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay
{
    public class GameplayUIManager : UIManager
    {
        private readonly Subject<Unit> _exitSceneRequest;


        public GameplayUIManager(ContainerBuilder builder) : base(builder) 
        {
            _exitSceneRequest = builder.Build().Resolve<Subject<Unit>>();
        }

        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this);
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public PopupPauseViewModel OpenPopupPause()
        {
            var Pause = new PopupPauseViewModel(_exitSceneRequest);
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenPopup(Pause);

            return Pause;
        }
    }
}