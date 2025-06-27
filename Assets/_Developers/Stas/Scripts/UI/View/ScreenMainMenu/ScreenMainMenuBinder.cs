using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu
{
    public class ScreenMainMenuBinder : ScreenBinder<ScreenMainMenuViewModel>
    {
        [SerializeField] private Button _goToScreenLevelSelectorButton;
        [SerializeField] private Button _goToScreenShopButton;
        [SerializeField] private Button _goToScreenLeaderboardButton;
        [SerializeField] private Button _goToScreenSettingsButton;

        [SerializeField] private List<Button> _serviceButtons;

        private void Awake()
        {
#if UNITY_EDITOR
            foreach (var button in _serviceButtons)
            {
                if (button != null)
                    button.gameObject.SetActive(true);
            }
#endif
        }

        private void OnEnable()
        {
            _goToScreenLevelSelectorButton.onClick.AddListener(OnGoToScreenLevelSelectorButtonClicked);
            _goToScreenShopButton.onClick.AddListener(OnGoToScreenShopButtonClicked);
            _goToScreenLeaderboardButton.onClick.AddListener(OnGoToScreenLeaderboardButtonClicked);
            _goToScreenSettingsButton.onClick.AddListener(OnGoToScreenSettingsButtonClicked);
        }

        private void OnDisable()
        {
            _goToScreenLevelSelectorButton.onClick.RemoveListener(OnGoToScreenLevelSelectorButtonClicked);
            _goToScreenShopButton.onClick.RemoveListener(OnGoToScreenShopButtonClicked);
            _goToScreenLeaderboardButton.onClick.RemoveListener(OnGoToScreenLeaderboardButtonClicked);
            _goToScreenSettingsButton.onClick.RemoveListener(OnGoToScreenSettingsButtonClicked);
        }

        private void OnGoToScreenLevelSelectorButtonClicked()
        {
            ViewModel.RequestGoToScreenLevelSelector();
        }

        private void OnGoToScreenShopButtonClicked()
        {
            ViewModel.RequestGoToScreenShop();
        }

        private void OnGoToScreenLeaderboardButtonClicked()
        {
            ViewModel.RequestGoToScreenLeaderboard();
        }

        private void OnGoToScreenSettingsButtonClicked()
        {
            ViewModel.RequestGoToScreenSettings();
        }
    }
}