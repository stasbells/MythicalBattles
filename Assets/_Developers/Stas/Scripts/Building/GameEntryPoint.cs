using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.MainMenu;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using R3;
using Reflex.Core;
using Reflex.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;

        private readonly Corutines _corutines;
        private readonly UIRootView _uiRoot;
        private readonly ContainerBuilder _rootContainer = new();

        private Container _cachedSceneContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _corutines = new GameObject("[CORUTINES]").AddComponent<Corutines>();
            Object.DontDestroyOnLoad(_corutines.gameObject);

            var prefabUIRoot = Resources.Load<UIRootView>("Prefabs/UI/UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);

            _rootContainer
                .AddSingleton(_uiRoot);
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                _corutines.StartCoroutine(LoadAndStart(Scenes.GAMEPLAY));
                return;
            }

            if (sceneName == Scenes.MAIN_MENU)
            {
                _corutines.StartCoroutine(LoadAndStart(Scenes.MAIN_MENU));
                return;
            }

            if (sceneName != Scenes.BOOT)
                return;
#endif
            _corutines.StartCoroutine(LoadAndStart(Scenes.MAIN_MENU));
        }

        private IEnumerator LoadAndStart(string sceneName)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(sceneName);

            //yield return new WaitForSeconds(1.0f);

            Object sceneEntryPoint = sceneName == Scenes.GAMEPLAY
                ? Object.FindFirstObjectByType<GameplayEntryPoint>()
                : Object.FindFirstObjectByType<MainMenuEntryPoint>();

            _cachedSceneContainer = new ContainerBuilder().SetParent
                (_rootContainer.Build()).Build();

            RunScene(sceneEntryPoint);

            _uiRoot.HideLoadingScreen();

            YandexGame.GameReadyAPI();
        }

        private void RunScene(Object sceneEntryPoint)
        {
            if (sceneEntryPoint is GameplayEntryPoint gameplayEntryPoint)
            {
                var signal = gameplayEntryPoint.Run(_cachedSceneContainer);

                signal.ExitSceneRequest.Subscribe(_ =>
                {
                    _corutines.StartCoroutine(LoadAndStart(GetSceneToLoad(Scenes.GAMEPLAY)));
                });

                signal.RestartSceneRequest.Subscribe(_ =>
                {
                    RestartSceneGamplay();
                });
            }
            else if (sceneEntryPoint is MainMenuEntryPoint mainMenuEntryPoint)
            {
                mainMenuEntryPoint.Run(_cachedSceneContainer).Subscribe(_ =>
                {
                    _corutines.StartCoroutine(LoadAndStart(GetSceneToLoad(Scenes.MAIN_MENU)));
                });
            }
        }

        private void RestartSceneGamplay()
        {
            _corutines.StartCoroutine(LoadAndStart(Scenes.GAMEPLAY));
        }

        private string GetSceneToLoad(string sceneName)
        {
            return sceneName == Scenes.GAMEPLAY ? Scenes.MAIN_MENU : Scenes.GAMEPLAY;
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}