using UnityEngine;

namespace MythicalBattles
{
    public class ScreenBinder<T> : MonoBehaviour, IScreenBinder where T : ScreenViewModel
    {
        protected T ViewModel;

        public void Bind(ScreenViewModel viewModel)
        {
            ViewModel = (T)viewModel;

            OnBind(ViewModel);
        }

        public virtual void Close()
        {
            Destroy(gameObject);
        }

        protected virtual void OnBind(T viewModel) { }
    }
}