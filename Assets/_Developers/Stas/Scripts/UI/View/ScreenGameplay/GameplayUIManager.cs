using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.Root.Gameplay;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupPause;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenDeath;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameComplete;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelComplete;
using Reflex.Core;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay
{
    public class GameplayUIManager : UIManager
    {
        //private readonly Subject<Unit> _exitSceneRequest;
        //private readonly Subject<Unit> _restartSceneRequest;

        private readonly Signal _signal;


        public GameplayUIManager(ContainerBuilder builder) : base(builder) 
        {
            //_exitSceneRequest = builder.Build().Resolve<Subject<Unit>>();

            _signal = builder.Build().Resolve<Signal>();
        }

        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this);
            
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }
        
        public ScreenLevelCompleteViewModel OpenScreenLevelComplete(
            float levelPassTime, float bestTime, int score, int rewardMoney)
        {
            var viewModel = new ScreenLevelCompleteViewModel(levelPassTime, bestTime, score, rewardMoney,
                _signal.ExitSceneRequest, _signal.RestartSceneRequest);
            
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenGameCompleteViewModel OpenScreenGameComplete()
        {
            var viewModel = new ScreenGameCompleteViewModel(_signal.ExitSceneRequest);
            
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }
        
        public ScreenDeathViewModel OpenScreenDeath()
        {
            var viewModel = new ScreenDeathViewModel(_signal.ExitSceneRequest, _signal.RestartSceneRequest);
            
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }
        
        public PopupPauseViewModel OpenPopupPause()
        {
            var Pause = new PopupPauseViewModel(_signal.ExitSceneRequest);
            
            var UIRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            UIRoot.OpenPopup(Pause);

            return Pause;
        }
    }
}