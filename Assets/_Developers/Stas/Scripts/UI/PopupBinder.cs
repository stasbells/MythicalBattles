using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI
{
    public abstract class PopupBinder<T> : ScreenBinder<T> where T : ScreenViewModel
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _closeAltButton;

        protected virtual void Start()
        {
            _closeButton?.onClick.AddListener(OnCloseButtonClick);
            _closeAltButton?.onClick.AddListener(OnCloseButtonClick);
        }

        protected virtual void OnDestroy()
        {
            _closeButton?.onClick.RemoveListener(OnCloseButtonClick);
            _closeAltButton?.onClick.RemoveListener(OnCloseButtonClick);
        }

        protected virtual void OnCloseButtonClick()
        {
            ViewModel.Close();
        }
    }
}