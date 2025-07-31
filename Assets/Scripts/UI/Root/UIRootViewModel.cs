using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MythicalBattles.Assets.Scripts.UI.Root
{
    public class UIRootViewModel : IDisposable
    {
        private readonly ReactiveProperty<ScreenViewModel> _openedScreen = new(null);
        private readonly ObservableList<ScreenViewModel> _openedPopups = new();
        private readonly Dictionary<ScreenViewModel, IDisposable> _popupSubscriptions = new();

        public ReadOnlyReactiveProperty<ScreenViewModel> OpenedScreen => _openedScreen;
        public IObservableCollection<ScreenViewModel> OpenedPopups => _openedPopups;

        public void Dispose()
        {
            CloseAllPopups();
            _openedScreen.Value?.Dispose();
        }

        public void OpenScreen(ScreenViewModel screenViewModel)
        {
            _openedScreen.Value?.Dispose();
            _openedScreen.Value = screenViewModel;
        }

        public void OpenPopup(ScreenViewModel popupViewModel)
        {
            if (_openedPopups.Contains(popupViewModel))
                return;

            if (_openedPopups.Any(popup => popup.Name == popupViewModel.Name))
                return;

            var subscription = popupViewModel.CloseReqested.Subscribe(ClosePopup);
            _popupSubscriptions.Add(popupViewModel, subscription);
            _openedPopups.Add(popupViewModel);
        }

        public void ClosePopup(ScreenViewModel popupViewModel)
        {
            if (_openedPopups.Contains(popupViewModel))
            {
                popupViewModel.Dispose();
                _openedPopups.Remove(popupViewModel);

                var subscription = _popupSubscriptions[popupViewModel];
                subscription?.Dispose();
                _popupSubscriptions.Remove(popupViewModel);
            }
        }

        public void CloseAllPopups()
        {
            foreach (var openedPopup in _openedPopups.ToList())
                ClosePopup(openedPopup);
        }
    }
}