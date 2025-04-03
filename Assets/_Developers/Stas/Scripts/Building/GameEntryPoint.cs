using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building
{
    class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Corutines _corutines;
        private UIRootView _uiRoot;

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

            _corutines.StartCoroutine(LoadAndStartGameplay());
        }

        private IEnumerator LoadAndStartMainMenu()
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);

            yield return new WaitForSeconds(2.0f);

            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            sceneEntryPoint.Run(_uiRoot);

            sceneEntryPoint.GoToGameplaySceneRequested += () =>
            {
                _corutines.StartCoroutine(LoadAndStartGameplay());
            };

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartGameplay()
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(2.0f);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Run(_uiRoot);

            sceneEntryPoint.GoToMainMenuSceneRequested += () =>
            {
                _corutines.StartCoroutine(LoadAndStartMainMenu());
            };

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}