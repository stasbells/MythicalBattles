using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.Screens
{
    public class MenuScreenBinder : ScreenBinder<MenuScreenViewModel>
    {
        [SerializeField] private Button _goToGameButton;

        private void OnEnable()
        {
            _goToGameButton.onClick.AddListener(OnGoToGameButtonClicked);
        }

        private void OnDisable()
        {
            _goToGameButton.onClick.RemoveListener(OnGoToGameButtonClicked);
        }

        private void OnGoToGameButtonClicked()
        {
            ViewModel.RequestGoToGame();
        }
    }
}
