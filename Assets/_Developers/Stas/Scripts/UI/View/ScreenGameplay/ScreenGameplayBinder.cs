using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay
{
    public class ScreenGameplayBinder : ScreenBinder<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _popupPauseButton;

        private void OnEnable()
        {
            _popupPauseButton.onClick.AddListener(OnPopupPauseButtonClicked);
        }

        private void OnDisable()
        {
            _popupPauseButton.onClick.RemoveListener(OnPopupPauseButtonClicked);
        }

        private void OnPopupPauseButtonClicked()
        {
            ViewModel.RequestGoToPopupPause();
        }
    }
}