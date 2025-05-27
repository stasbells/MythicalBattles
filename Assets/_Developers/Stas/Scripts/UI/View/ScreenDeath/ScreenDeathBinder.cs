using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenDeath
{
    public class ScreenDeathBinder : ScreenBinder<ScreenDeathViewModel>
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _retryButton;

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _retryButton.onClick.RemoveListener(OnRetryButtonClicked);
        }
        
        private void OnMainMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }
        
        private void OnRetryButtonClicked()
        {
            ViewModel.RequestToRestartLevel();
        }
    }
}
