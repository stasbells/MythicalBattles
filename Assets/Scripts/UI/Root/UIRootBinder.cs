using ObservableCollections;
using R3;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.Root
{
    public class UIRootBinder : MonoBehaviour
    {
        private readonly CompositeDisposable _subscriptions = new();

        [SerializeField] private ScreensContainer _screensContainer;

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }

        public void Bind(UIRootViewModel viewModel)
        {
            _subscriptions.Add(viewModel.OpenedScreen.Subscribe(newScreenViewModel =>
            {
                _screensContainer.OpenScreen(newScreenViewModel);
            }));

            foreach (var openedPopup in viewModel.OpenedPopups)
            {
                _screensContainer.OpenPopup(openedPopup);
            }

            _subscriptions.Add(viewModel.OpenedPopups.ObserveAdd().Subscribe(e =>
            {
                _screensContainer.OpenPopup(e.Value);
            }));

            _subscriptions.Add(viewModel.OpenedPopups.ObserveRemove().Subscribe(e =>
            {
                _screensContainer.ClosePopup(e.Value);
            }));

            OnBind(viewModel);
        }

        protected virtual void OnBind(UIRootViewModel viewModel) { }
    }
}
