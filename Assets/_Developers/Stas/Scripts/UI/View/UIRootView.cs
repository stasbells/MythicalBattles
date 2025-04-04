using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Transform _sceneUIContainer;

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
        }

        private void ClearSceneUI()
        {
            var childCount = _sceneUIContainer.childCount;

            for (var i = 0; i < childCount; i++)
            {
                Destroy(_sceneUIContainer.GetChild(i).gameObject);
            }
        }
    }
}
