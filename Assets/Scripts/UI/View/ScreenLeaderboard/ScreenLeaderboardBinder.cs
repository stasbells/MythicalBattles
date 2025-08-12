using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenLeaderboard
{
    public class ScreenLeaderboardBinder : ScreenBinder<ScreenLeaderboardViewModel>
    {
        [SerializeField] private Button _goToScreenMainMenuButton;

        private void OnEnable()
        {
            _goToScreenMainMenuButton.onClick.AddListener(OnGoToScreenMainMenuButtonClicked);
        }

        private void OnDisable()
        {
            _goToScreenMainMenuButton.onClick.RemoveListener(OnGoToScreenMainMenuButtonClicked);
        }

        private void OnGoToScreenMainMenuButtonClicked()
        {
            ViewModel.RequestGoToScreenMainMenu();
        }
    }
}