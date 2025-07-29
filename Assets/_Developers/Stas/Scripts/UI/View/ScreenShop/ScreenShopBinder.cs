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

        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _persistentData = container.Resolve<IPersistentData>();
            _dataProvider = container.Resolve<IDataProvider>();
            _playerStats = container.Resolve<IPlayerStats>();
        }

        private void Awake()
        {
            Construct();

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

        protected override void OnBind(ScreenShopViewModel viewModel)
        {
            viewModel.ShopPanel.OnNext(_shop.ShopPanel);
            viewModel.OnShopPanelChanged(_shop.ShopPanel, _inventory);
        }

        private void OnGoToScreenMainMenuButtonClicked()
        {
            ViewModel.RequestGoToScreenMainMenu();
        }

        private void LoadOrInitPlayerData()
        {
            _dataProvider.LoadPlayerData();
            _persistentData.PlayerData.Initialize(_shop.ItemsContent);
            _playerStats.UpdatePlayerData(_persistentData.PlayerData);
        }
    }
}