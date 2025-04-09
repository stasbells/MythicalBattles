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

        public Observable<Unit> Run(Container gameplayContainer)
        {
            var uiScene = Instantiate(_sceneUIRootPrefab);

            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignal = new Subject<Unit>();
            uiScene.Bind(exitSceneSignal);

            return exitSceneSignal.AsObservable();
        }
    }
}
