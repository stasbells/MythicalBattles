using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI
{
    public class ScreensContainer : MonoBehaviour
    {
        [SerializeField] private Transform _screensContainer;
        [SerializeField] private Transform _popupsContainer;

        private readonly Dictionary<ScreenViewModel, IScreenBinder> _openedPopupBinders = new();
        private IScreenBinder _openedScreenBinder;

        public void OpenPopup(ScreenViewModel viewModel)
        {
            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);
            var createdPopup = Instantiate(prefab, _popupsContainer);
            var binder = createdPopup.GetComponent<IScreenBinder>();

            binder.Bind(viewModel);
            _openedPopupBinders.Add(viewModel, binder);
        }

        public void ClosePopup(ScreenViewModel popupViewModel)
        {
            var binder = _openedPopupBinders[popupViewModel];

            binder?.Close();
            _openedPopupBinders.Remove(popupViewModel);
        }

        public void OpenScreen(ScreenViewModel viewModel)
        {
            if(viewModel == null)
                return;

            _openedScreenBinder?.Close();

            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);
            var createdScreen = Instantiate(prefab, _screensContainer);
            var binder = createdScreen.GetComponent<IScreenBinder>();

            binder.Bind(viewModel);
            _openedScreenBinder = binder;
        }

        private static string GetPrefabPath(ScreenViewModel viewModel)
        {
            return $"Prefabs/UI/{viewModel.Name}";
        }
    }
}