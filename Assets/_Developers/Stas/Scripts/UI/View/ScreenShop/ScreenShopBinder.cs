using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenShop
{
    public class ScreenShopBinder : ScreenBinder<ScreenShopViewModel>
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private InventoryView _inventory;
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
            if (_dataProvider.TryLoadPlayerData() == false)
            {
                _persistentData.PlayerData = new PlayerData();

                _dataProvider.SavePlayerData();
            }

            _persistentData.PlayerData.Initialize(_shop.ItemsContent);

            _playerStats.UpdatePlayerData(_persistentData.PlayerData);

            if (_dataProvider.TryLoadGameProgressData() == false)   // временно для теста потом перенести в бутстрап общий
            {
                _persistentData.GameProgressData = new GameProgressData();

                _dataProvider.SaveGameProgressData();
            }

            if (_dataProvider.TryLoadSettingsData() == false)       // временно для теста потом перенести в бутстрап общий
            {
                _persistentData.SettingsData = new SettingsData();

                _dataProvider.SaveSettingsData();
            }
        }

        protected override void OnBind(ScreenShopViewModel viewModel)
        {
            viewModel.ShopPanel.OnNext(_shop.ShopPanel);

            Debug.Log($"ScreenShopBinder: ShopPanel: {viewModel.ShopPanel.Value}");

            viewModel.OnShopPanelChanged(_shop.ShopPanel, _inventory);
        }
    }
}