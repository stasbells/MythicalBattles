using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.Root.Gameplay;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;
using R3;
using Reflex.Core;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        [SerializeField] private WorldGameplayRootBinder _worldRootBinder;

        //private Container _gameplayContainer;

        public Observable<Unit> Run(Container projectContainer)
        {
            //_gameplayContainer = new ContainerBuilder().SetParent(gameplayContainer)
                //.AddSingleton(new Subject<Unit>())
                //.Build();

            var gameplayContainer = new ContainerBuilder().SetParent(projectContainer);

            gameplayContainer
                .AddSingleton(new Subject<Unit>())
                .AddSingleton(new GameplayUIManager(gameplayContainer))
                .AddSingleton(typeof(LevelGeneratorViewModel))
                .AddSingleton(typeof(WorldGameplayRootViewModel))
                .AddSingleton(typeof(UIGameplayRootViewModel));
                
            InitUI(gameplayContainer);
            InitWorld(gameplayContainer.Build());

            var exitSceneSignal = gameplayContainer.Build().Resolve<Subject<Unit>>();

            return exitSceneSignal.AsObservable();
        }

        private void InitWorld(Container gamplayContainer)
        {
            _worldRootBinder.Bind(gamplayContainer.Resolve<WorldGameplayRootViewModel>());
        }

        private void InitUI(ContainerBuilder gameplayContainer)
        {
            var uiRoot = gameplayContainer.Build().Resolve<UIRootView>();

            var uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

            var canvas = uiRoot.GetComponentInChildren<Canvas>();
            gameplayContainer.AddSingleton(canvas);

            var uiSceneRootViewModel = gameplayContainer.Build().Resolve<UIGameplayRootViewModel>();
            uiSceneRootBinder.Bind(uiSceneRootViewModel);

            var uiManager = gameplayContainer.Build().Resolve<GameplayUIManager>();
            uiManager.OpenScreenGameplay();
        }
    }
}