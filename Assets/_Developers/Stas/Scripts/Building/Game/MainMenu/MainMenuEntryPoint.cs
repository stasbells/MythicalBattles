using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Services;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu;
using R3;
using Reflex.Core;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root
{
    class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

        private Container _mainMenuContainer;

        public Observable<Unit> Run(Container mainMenuContainer)
        {
            _mainMenuContainer = new ContainerBuilder().SetParent(mainMenuContainer)
                .AddSingleton(new Subject<Unit>())
                .Build();

            var mainMenuViewModelsContainer = new ContainerBuilder().SetParent(mainMenuContainer);

            mainMenuViewModelsContainer
                .AddSingleton(new Subject<Unit>())
                .AddSingleton(new MainMenuUIManager(mainMenuViewModelsContainer))
                .AddSingleton(typeof(UIMainMenuRootViewModel))
                .Build();

            InitUI(mainMenuViewModelsContainer.Build());

            var exitSceneSignal = mainMenuViewModelsContainer.Build().Resolve<Subject<Unit>>();

            return exitSceneSignal.AsObservable();
        }

        private void InitUI(Container viewsContainer)
        {
            var uiRoot = viewsContainer.Resolve<UIRootView>();
            var uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

            var uiSceneRootViewModel = viewsContainer.Resolve<UIMainMenuRootViewModel>();
            uiSceneRootBinder.Bind(uiSceneRootViewModel);

            var uiManager = viewsContainer.Resolve<MainMenuUIManager>();
            uiManager.OpenScreenMainMenu();
        }
    }
}