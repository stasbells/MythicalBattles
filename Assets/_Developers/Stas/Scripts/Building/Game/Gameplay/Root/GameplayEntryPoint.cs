using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using UnityEngine;
using System;
using Reflex.Core;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root
{
    class GameplayEntryPoint : MonoBehaviour
    {
        public event Action GoToMainMenuSceneRequested;

        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;

        public void Run(Container gameplayContainer)
        {
            var uiScene = Instantiate(_sceneUIRootPrefab);

            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            uiRoot.AttachSceneUI(uiScene.gameObject);

            uiScene.GoToMainMenuButtonClicked += () =>
            {
                GoToMainMenuSceneRequested?.Invoke();
            };
        }
    }
}
