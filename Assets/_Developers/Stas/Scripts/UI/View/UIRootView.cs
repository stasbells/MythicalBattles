using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Transform _sceneUIContainer;
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            _loadingScreen.SetActive(false);
        }

        public void AttachSceneUI(GameObject sceneUI)
        {
            ClearSceneUI();

            sceneUI.transform.SetParent(_sceneUIContainer, false);
            sceneUI.GetComponentInChildren<VirtualJoystick>()?
                .SetCanvas(GetComponentInChildren<Canvas>());
        }

        private void ClearSceneUI()
        {
            var childCount = _sceneUIContainer.childCount;
            var progressBar = _canvas.GetComponentInChildren<WaveProgressView>();

            for (var i = 0; i < childCount; i++)
                Destroy(_sceneUIContainer.GetChild(i).gameObject);

            if (progressBar != null)
                Destroy(_canvas.GetComponentInChildren<WaveProgressView>().gameObject);
        }
    }
}