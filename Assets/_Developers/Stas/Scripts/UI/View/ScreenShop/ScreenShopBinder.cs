using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenShop
{
    public class ScreenShopBinder : ScreenBinder<ScreenShopViewModel>
    {
        [SerializeField] private Shop _shop;
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

<<<<<<< HEAD
                _dataProvider.Save();
=======
               _dataProvider.SavePlayerData();
>>>>>>> 9b9d89981aabef446a7fa19365ca94737c708b5e
            }

            _persistentData.PlayerData.Initialize(_shop.ItemsContent);

            _playerStats.UpdatePlayerData(_persistentData.PlayerData);
        }

        protected override void OnBind(ScreenShopViewModel viewModel)
        {
            viewModel.ShopPanel.OnNext(_shop.ShopPanel);

            Debug.Log($"ScreenShopBinder: ShopPanel: {viewModel.ShopPanel.Value}");

            viewModel.OnShopPanelChanged(_shop.ShopPanel);
        }
    }
}