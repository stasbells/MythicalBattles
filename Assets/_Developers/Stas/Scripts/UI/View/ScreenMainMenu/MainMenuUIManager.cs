using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using R3;
using Reflex.Core;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu
{
    public class MainMenuUIManager : UIManager
    {
        private readonly Subject<Unit> _exitSceneRequest;
        public MainMenuUIManager(ContainerBuilder builder) : base(builder)
        {
            _exitSceneRequest = builder.Build().Resolve<Subject<Unit>>();
        }

        public ScreenMainMenuViewModel OpenScreenMainMenu()
        {
            var viewModel = new ScreenMainMenuViewModel(this, _exitSceneRequest);
            var UIRoot = Container.Build().Resolve<UIMainMenuRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }
    } 
}