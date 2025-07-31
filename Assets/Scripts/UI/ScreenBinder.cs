using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI
{
    public class ScreenBinder<T> : MonoBehaviour, IScreenBinder where T : ScreenViewModel
    {
        private T _viewModel;

        public T ViewModel => _viewModel;

        public void Bind(ScreenViewModel viewModel)
        {
            _viewModel = (T)viewModel;

            OnBind(_viewModel);
        }

        public virtual void Close()
        {
            Destroy(gameObject);
        }

        protected virtual void OnBind(T viewModel) { }
    }
}