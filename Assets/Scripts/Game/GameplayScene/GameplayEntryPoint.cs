using MythicalBattles.Assets.Scripts.Game.GameplayScene.Root;
using MythicalBattles.Assets.Scripts.GameplayScene;
using MythicalBattles.Assets.Scripts.UI.Root.Gameplay;
using MythicalBattles.Assets.Scripts.UI.View;
using MythicalBattles.Assets.Scripts.UI.View.ScreenGameplay;
using MythicalBattles.Assets.Scripts.Utils;
using Reflex.Core;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Game.GameplayScene
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        [SerializeField] private WorldGameplayRootBinder _worldRootBinder;

        public Signal Run(Container projectContainer)
        {
            var gameplayContainer = new ContainerBuilder().SetParent(projectContainer);

            gameplayContainer
                .AddSingleton(new Signal())
                .AddSingleton(new GameplayUIManager(gameplayContainer))
                .AddSingleton(new WorldGameplayRootViewModel(gameplayContainer.Build()))
                .AddSingleton(typeof(LevelGeneratorViewModel))
                .AddSingleton(typeof(UIGameplayRootViewModel));

            InitUI(gameplayContainer);
            InitWorld(gameplayContainer.Build());

            return gameplayContainer.Build().Resolve<Signal>();
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