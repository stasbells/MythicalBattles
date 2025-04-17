using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
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
                .AddSingleton(typeof(SpawnPointGenerator), typeof(ISpawnPointGenerator))
                .Build();

            var mainMenuViewModelsContainer = new ContainerBuilder().SetParent(mainMenuContainer)
                .AddSingleton(typeof(UIMainMenuRootViewModel))
                .Build();

            var sceneUI = Instantiate(_sceneUIRootPrefab);

            var uiRoot = _mainMenuContainer.Resolve<UIRootView>();
            uiRoot.AttachSceneUI(sceneUI.gameObject);

            var exitSceneSignal = new Subject<Unit>();
            sceneUI.Bind(exitSceneSignal);

            return exitSceneSignal.AsObservable();
        }
    }
}