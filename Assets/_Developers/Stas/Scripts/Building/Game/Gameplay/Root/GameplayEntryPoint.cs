using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using UnityEngine;
using Reflex.Core;
using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root
{
    class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        [SerializeField] private WorldGameplayRootBinder _worldGameplayRootPrefab;

        public Observable<Unit> Run(Container gameplayContainer)
        {
            var sceneUI = Instantiate(_sceneUIRootPrefab);
            var worldGameplay = Instantiate(_worldGameplayRootPrefab);

            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            uiRoot.AttachSceneUI(sceneUI.gameObject);

            var gameplayRoot = gameplayContainer.Resolve<WorldGameplayRootView>();
            gameplayRoot.AttachWorldGameplay(worldGameplay.gameObject);

            var exitSceneSignal = new Subject<Unit>();
            sceneUI.Bind(exitSceneSignal);

            return exitSceneSignal.AsObservable();
        }
    }
}