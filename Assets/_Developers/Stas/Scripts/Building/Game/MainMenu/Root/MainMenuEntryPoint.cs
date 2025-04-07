using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using UnityEngine;
using System;
using Reflex.Core;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root
{
    class MainMenuEntryPoint : MonoBehaviour
    {
        public event Action GoToGameplaySceneRequested;

        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

        public void Run(Container mainMenuContainer)
        {
            var uiScene = Instantiate(_sceneUIRootPrefab);
            var uiRoot = mainMenuContainer.Resolve<UIRootView>();
            uiRoot.AttachSceneUI(uiScene.gameObject);

            uiScene.GoToGameplayButtonClicked += () =>
            {
                GoToGameplaySceneRequested?.Invoke();
            };
        }
    }
}
