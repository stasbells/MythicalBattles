using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using Reflex.Core;
using R3;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private readonly Corutines _corutines;
        private readonly UIRootView _uiRoot;
        private readonly WorldGameplayRootView _worldGameplayRoot;
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

            var prefabWorldGamplayRoot = Resources.Load<WorldGameplayRootView>("Prefabs/UI/WorldGameplayRoot");
            _worldGameplayRoot = Object.Instantiate(prefabWorldGamplayRoot);
            Object.DontDestroyOnLoad(_worldGameplayRoot.gameObject);

            _rootContainer.AddSingleton(_uiRoot);
            _rootContainer.AddSingleton(_worldGameplayRoot);
            _rootContainer.AddTransient(typeof(SpawnPointGenerator), typeof(ISpawnPointGenerator));
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            Debug.Log("Game started");
#endif
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                _corutines.StartCoroutine(LoadAndStartGameplay());
                return;
            }

            if (sceneName == Scenes.MAIN_MENU)
            {
                _corutines.StartCoroutine(LoadAndStartMainMenu());
                return;
            }

            if (sceneName != Scenes.BOOT)
                return;

            _corutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartMainMenu()
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);

            yield return new WaitForSeconds(1.0f);

            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();

            var mainMenuContainer = _cachedSceneContainer = new ContainerBuilder().SetParent(_rootContainer.Build()).Build();

            sceneEntryPoint.Run(mainMenuContainer).Subscribe(_ =>
            {
                _corutines.StartCoroutine(LoadAndStartGameplay());
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartGameplay()
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(1.0f);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();

            var gameplayContainer = _cachedSceneContainer = new ContainerBuilder().SetParent(_rootContainer.Build()).Build();

            sceneEntryPoint.Run(gameplayContainer).Subscribe(_ =>
            {
                _corutines.StartCoroutine(LoadAndStartMainMenu());
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}