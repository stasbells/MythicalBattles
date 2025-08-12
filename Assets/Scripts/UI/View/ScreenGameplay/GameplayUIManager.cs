using System;
using MythicalBattles.Assets.Scripts.UI.Root.Gameplay;
using MythicalBattles.Assets.Scripts.UI.View.PopupPause;
using MythicalBattles.Assets.Scripts.UI.View.ScreenDeath;
using MythicalBattles.Assets.Scripts.UI.View.ScreenGameComplete;
using MythicalBattles.Assets.Scripts.UI.View.ScreenLevelComplete;
using MythicalBattles.Assets.Scripts.Utils;
using R3;
using Reflex.Core;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenGameplay
{
    public class GameplayUIManager : UIManager
    {
        private const float DeathScreenDelay = 2.5f;

        private readonly Signal _signal;
        private readonly CompositeDisposable _disposable = new ();

        public GameplayUIManager(ContainerBuilder builder) : base(builder)
        {
            _signal = builder.Build().Resolve<Signal>();
        }

        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this);

            var uiRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            uiRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenLevelCompleteViewModel OpenScreenLevelComplete(
            float levelPassTime, float bestTime, int score, int rewardMoney)
        {
            var viewModel = new ScreenLevelCompleteViewModel(
                this,
                levelPassTime,
                bestTime,
                score,
                rewardMoney,
                _signal.ExitSceneRequest,
                _signal.RestartSceneRequest);

            var uiRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            uiRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenGameCompleteViewModel OpenScreenGameComplete()
        {
            var viewModel = new ScreenGameCompleteViewModel(_signal.ExitSceneRequest);

            var uiRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            uiRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public PopupPauseViewModel OpenPopupPause()
        {
            var pause = new PopupPauseViewModel(_signal.ExitSceneRequest);

            var uiRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            uiRoot.OpenPopup(pause);

            return pause;
        }

        public void SubscribeOnPlayerDeath(ReadOnlyReactiveProperty<bool> isDead)
        {
            isDead.Subscribe(OnPlayerDeath).AddTo(_disposable);
        }

        private void OnPlayerDeath(bool isDead)
        {
            if (isDead)
            {
                Observable.Timer(TimeSpan.FromSeconds(DeathScreenDelay))
                    .Subscribe(_ => ShowDeathScreen())
                    .AddTo(_disposable);
            }
        }

        private void ShowDeathScreen()
        {
            OpenScreenDeath();

            _disposable.Dispose();
        }

        private ScreenDeathViewModel OpenScreenDeath()
        {
            var viewModel = new ScreenDeathViewModel(_signal.ExitSceneRequest, _signal.RestartSceneRequest);

            var uiRoot = Container.Build().Resolve<UIGameplayRootViewModel>();

            uiRoot.OpenScreen(viewModel);

            return viewModel;
        }
    }
}