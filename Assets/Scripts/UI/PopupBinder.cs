using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets.Scripts.UI
{
    public abstract class PopupBinder<T> : ScreenBinder<T> where T : ScreenViewModel
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _closeAltButton;

        private void Start()
        {
            _closeButton?.onClick.AddListener(OnCloseButtonClick);
            _closeAltButton?.onClick.AddListener(OnCloseButtonClick);

            OnPopupBinderStart();
        }

        private void OnDestroy()
        {
            _closeButton?.onClick.RemoveListener(OnCloseButtonClick);
            _closeAltButton?.onClick.RemoveListener(OnCloseButtonClick);

            OnPopupBinderDestroy();
        }

        protected virtual void OnCloseButtonClick()
        {
            ViewModel.Close();
        }

        protected virtual void OnPopupBinderStart() { }

        protected virtual void OnPopupBinderDestroy() { }
    }
}