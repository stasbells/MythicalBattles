using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay
{
    public class ScreenGameplayBinder : ScreenBinder<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _goToMenu;

        private void OnEnable()
        {
            _goToMenu.onClick.AddListener(OnGoToMenuButtonClicked);
        }

        private void OnDisable()
        {
            _goToMenu.onClick.RemoveListener(OnGoToMenuButtonClicked);
        }

        private void OnGoToMenuButtonClicked()
        {
            ViewModel.RequestGoToMenu();
        }
    }
}
