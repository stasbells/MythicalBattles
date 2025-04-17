using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay
{
    public class ScreenGameplayBinder : ScreenBinder<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _goToMainMenuButton;
        [SerializeField] private Button _popupAButton;
        [SerializeField] private Button _popupBButton;

        private void OnEnable()
        {
            _goToMainMenuButton.onClick.AddListener(OnGoToMenuButtonClicked);
            _popupAButton.onClick.AddListener(OnPopupAButtonClicked);
            _popupBButton.onClick.AddListener(OnPopupBButtonClicked);
        }

        private void OnDisable()
        {
            _goToMainMenuButton.onClick.RemoveListener(OnGoToMenuButtonClicked);
            _popupAButton.onClick.RemoveListener(OnPopupAButtonClicked);
            _popupBButton.onClick.RemoveListener(OnPopupBButtonClicked);
        }

        private void OnGoToMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }

        private void OnPopupAButtonClicked()
        {
            ViewModel.RequestGoToPopupA();
        }

        private void OnPopupBButtonClicked()
        {
            ViewModel.RequestGoToPopupB();
        }
    }
}
