using UnityEngine;
using UnityEngine.UI;
using YG;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupPause
{
    public class PopupPauseBinder : PopupBinder<PopupPauseViewModel>
    {
        [SerializeField] private Button _goToMainMenuButton;

        private void OnEnable()
        {
            _goToMainMenuButton.onClick.AddListener(OnGoToMainMenuButtonClicked);
            YG2.onShowWindowGame += OnShowWindowGame;

            OnPause();
        }

        private void OnDisable()
        {
            _goToMainMenuButton.onClick.RemoveListener(OnGoToMainMenuButtonClicked);
            YG2.onShowWindowGame -= OnShowWindowGame;

            OnPlay();
        }

        private void OnGoToMainMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }

        private void OnShowWindowGame()
        {
            OnPause();
        }

        private void OnPause()
        {
            Time.timeScale = 0f;
            AudioListener.volume = 0f;
        }

        private void OnPlay()
        {
            Time.timeScale = 1f;
            AudioListener.volume = 1f;
        }
    }
}