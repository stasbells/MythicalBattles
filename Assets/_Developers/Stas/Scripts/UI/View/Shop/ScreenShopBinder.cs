using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMenu;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.Shop
{
    public class ScreenShopBinder : ScreenBinder<ScreenShopViewModel>
    {
        [SerializeField] private MythicalBattles.Shop _shop;
        [SerializeField] private Button _goToScreenMainMenuButton;

        private IDataProvider _dataProvider;
        private IPersistentData _persistentData;
        private IPlayerStats _playerStats;

        private void Awake()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _persistentData = container.Resolve<IPersistentData>();
            _dataProvider = container.Resolve<IDataProvider>();
            _playerStats = container.Resolve<IPlayerStats>();

            _shop.ItemsContent.InitializeRegistry();

            LoadOrInitPlayerData();
        }

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

        private void LoadOrInitPlayerData()
        {
            //if (_dataProvider.TryLoad() == false)
            //{
            //    _persistentData.PlayerData = new PlayerData();

            //   _dataProvider.Save();
            //}

            _persistentData.PlayerData = new PlayerData();

            _dataProvider.Save();

            _persistentData.PlayerData.Initialize(_shop.ItemsContent);

            _playerStats.UpdatePlayerData(_persistentData.PlayerData);
        }
    }
}