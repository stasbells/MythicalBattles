using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using UnityEngine;
using Reflex.Core;
using R3;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root
{
    class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        [SerializeField] private WorldGameplayRootBinder _worldRootBinder;

        private Container _gameplayContainer;

        public Observable<Unit> Run(Container gameplayContainer)
        {
            _gameplayContainer = new ContainerBuilder().SetParent(gameplayContainer)
                .AddSingleton(new Subject<Unit>())
                .Build();

            var gameplayViewModelsContainer = new ContainerBuilder().SetParent(gameplayContainer);

            gameplayViewModelsContainer
                .AddSingleton(new Subject<Unit>())
                .AddSingleton(new GameplayUIManager(gameplayViewModelsContainer))
                .AddSingleton(typeof(MapViewModel))
                .AddSingleton(typeof(WorldGameplayRootViewModel))
                .AddSingleton(typeof(UIGameplayRootViewModel))
                .Build();

            InitWorld(gameplayViewModelsContainer.Build());
            InitUI(gameplayViewModelsContainer.Build());

            var exitSceneSignal = gameplayViewModelsContainer.Build().Resolve<Subject<Unit>>();

            return exitSceneSignal.AsObservable();
        }

        private void InitWorld(Container viewsContainer)
        {
            _worldRootBinder.Bind(viewsContainer.Resolve<WorldGameplayRootViewModel>());
        }

        private void InitUI(Container viewsContainer)
        {
            var uiRoot = viewsContainer.Resolve<UIRootView>();
            var uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

            var uiSceneRootViewModel = viewsContainer.Resolve<UIGameplayRootViewModel>();
            uiSceneRootBinder.Bind(uiSceneRootViewModel);

            var uiManager = viewsContainer.Resolve<GameplayUIManager>();
            uiManager.OpenScreenGameplay();
        }
    }
}