using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.MainMenu;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using R3;
using Reflex.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building
{
    public class GameEntryPoint
    {
        private static GameEntryPoint s_instance;

        private readonly Corutines _corutines;
        private readonly UIRootView _uiRoot;
        private readonly ContainerBuilder _rootContainer = new();

        private Container _cachedSceneContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            s_instance = new GameEntryPoint();
            s_instance.RunGame();
        }

        private GameEntryPoint()
        {
            _corutines = new GameObject("[CORUTINES]").AddComponent<Corutines>();
            Object.DontDestroyOnLoad(_corutines.gameObject);

            var prefabUIRoot = Resources.Load<UIRootView>("Prefabs/UI/UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);

            _rootContainer.AddSingleton(_uiRoot);
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.Gameplay)
            {
                _corutines.StartCoroutine(LoadAndStart(Scenes.Gameplay));
                return;
            }

            if (sceneName == Scenes.MainMenu)
            {
                _corutines.StartCoroutine(LoadAndStart(Scenes.MainMenu));
                return;
            }

            if (sceneName != Scenes.Boot)
                return;
#endif
            _corutines.StartCoroutine(LoadAndStart(Scenes.MainMenu));
        }

        private IEnumerator LoadAndStart(string sceneName)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.Boot);
            yield return LoadScene(sceneName);

            Object sceneEntryPoint = sceneName == Scenes.Gameplay
                ? Object.FindFirstObjectByType<GameplayEntryPoint>()
                : Object.FindFirstObjectByType<MainMenuEntryPoint>();

            _cachedSceneContainer = new ContainerBuilder().SetParent
                (_rootContainer.Build()).Build();

            RunScene(sceneEntryPoint);

            _uiRoot.HideLoadingScreen();
        }

        private void RunScene(Object sceneEntryPoint)
        {
            if (sceneEntryPoint is GameplayEntryPoint gameplayEntryPoint)
            {
                var signal = gameplayEntryPoint.Run(_cachedSceneContainer);

                signal.ExitSceneRequest.Subscribe(_ =>
                {
                    _corutines.StartCoroutine(LoadAndStart(GetSceneToLoad(Scenes.Gameplay)));
                });

                signal.RestartSceneRequest.Subscribe(_ =>
                {
                    RestartSceneGameplay();
                });
            }
            else if (sceneEntryPoint is MainMenuEntryPoint mainMenuEntryPoint)
            {
                mainMenuEntryPoint.Run(_cachedSceneContainer).Subscribe(_ =>
                {
                    _corutines.StartCoroutine(LoadAndStart(GetSceneToLoad(Scenes.MainMenu)));
                });
            }
        }

        private void RestartSceneGameplay()
        {
            _corutines.StartCoroutine(LoadAndStart(Scenes.Gameplay));
        }

        private string GetSceneToLoad(string sceneName)
        {
            return sceneName == Scenes.Gameplay ? Scenes.MainMenu : Scenes.Gameplay;
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}