using UnityEngine;
using UnityEngine.UI;
using YG;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay
{
    public class ScreenGameplayBinder : ScreenBinder<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _popupPauseButton;

        private void OnEnable()
        {
            _popupPauseButton.onClick.AddListener(OnPopupPauseButtonClicked);
            YG2.onHideWindowGame += OnHideWindowGame;
        }

        private void OnDisable()
        {
            _popupPauseButton.onClick.RemoveListener(OnPopupPauseButtonClicked);
            YG2.onHideWindowGame -= OnHideWindowGame;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
                ViewModel.RequestGoToPopupPause();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                ViewModel.RequestGoToPopupPause();
        }

        private void OnHideWindowGame()
        {
            ViewModel.RequestGoToPopupPause();
        }

        private void OnPopupPauseButtonClicked()
        {
            ViewModel.RequestGoToPopupPause();
        }
    }
}