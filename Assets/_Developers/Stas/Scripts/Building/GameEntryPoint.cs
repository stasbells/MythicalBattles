using UnityEngine;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using UnityEngine.SceneManagement;
using System.Collections;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root;

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

            if (sceneName == Constants.MainMenu)
            {
                _corutines.StartCoroutine(LoadAndStartGameplay());
                return;
            }

            if (sceneName != Constants.Boot)
                return;

            _corutines.StartCoroutine(LoadAndStartGameplay());
        }

        private IEnumerator LoadAndStartGameplay()
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Constants.Boot);
            yield return LoadScene(Constants.MainMenu);

            yield return new WaitForSeconds(2.0f);

            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            sceneEntryPoint.Run();

            _uiRoot.HideLoadingScreen();

        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}