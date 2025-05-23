using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupA
{
    public class PopupPauseBinder : PopupBinder<PopupPauseViewModel>
    {
        [SerializeField] private Button _goToMainMenuButton;

        private void OnEnable()
        {
            _goToMainMenuButton.onClick.AddListener(OnGoToMainMenuButtonClicked);

            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
            _goToMainMenuButton.onClick.RemoveListener(OnGoToMainMenuButtonClicked);
        }

        private void OnGoToMainMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }
    }
}