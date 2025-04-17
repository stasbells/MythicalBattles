using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupA;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupB;
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
            var viewModel = new ScreenGameplayViewModel(this, _exitSceneRequest);
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public PopupAViewModel OpenPopupA()
        {
            var a = new PopupAViewModel();
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenPopup(a);

            return a;
        }

        public PopupBViewModel OpenPopupB()
        {
            var b = new PopupBViewModel();
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenPopup(b);

            return b;
        }
    }
}