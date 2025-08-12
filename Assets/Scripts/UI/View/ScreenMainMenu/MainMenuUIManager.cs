using MythicalBattles.Assets.Scripts.Shop;
using MythicalBattles.Assets.Scripts.UI.Root.MainMenu;
using MythicalBattles.Assets.Scripts.UI.View.PopupEquipmentItem;
using MythicalBattles.Assets.Scripts.UI.View.PopupShopItem;
using MythicalBattles.Assets.Scripts.UI.View.ScreenLeaderboard;
using MythicalBattles.Assets.Scripts.UI.View.ScreenLevelSelector;
using MythicalBattles.Assets.Scripts.UI.View.ScreenSettings;
using MythicalBattles.Assets.Scripts.UI.View.ScreenShop;
using MythicalBattles.Assets.Scripts.UI.View.ScreenTutorial;
using R3;
using Reflex.Core;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenMainMenu
{
    public class MainMenuUIManager : UIManager
    {
        private readonly Subject<Unit> _exitSceneRequest;
        private readonly ReactiveProperty<ShopPanel> _shopPanel = new ();

        public MainMenuUIManager(ContainerBuilder builder) 
            : base(builder)
        {
            _exitSceneRequest = builder.Build().Resolve<Subject<Unit>>();
        }

        public ScreenLevelSelectorViewModel OpenScreenLevelSelector()
        {
            var viewModel = new ScreenLevelSelectorViewModel(this, _exitSceneRequest);
            
            OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenMainMenuViewModel OpenScreenMainMenu()
        {
            var viewModel = new ScreenMainMenuViewModel(this);
            
            OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenShopViewModel OpenScreenShop()
        {
            var viewModel = new ScreenShopViewModel(this, _shopPanel);
            
            OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenTutorialViewModel OpenScreenTutorial()
        {
            var viewModel = new ScreenTutorialViewModel(_exitSceneRequest);
            
            OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenLeaderboardViewModel OpenScreenLeaderboard()
        {
            var viewModel = new ScreenLeaderboardViewModel(this);
            
            OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenSettingsViewModel OpenScreenSettings()
        {
            var viewModel = new ScreenSettingsViewModel(this);

            OpenScreen(viewModel);

            return viewModel;
        }

        public PopupShopItemViewModel OpenPopupShopItem(ShopPanel shopPanel, ShopItemView shopItemView)
        {
            var shopItem = new PopupShopItemViewModel(shopPanel, shopItemView);
            
            OpenPopup(shopItem);

            return shopItem;
        }

        public PopupEquipmentItemViewModel OpenPopupEquipmentItem(InventoryItemView inventoryItemView)
        {
            var inventoryItem = new PopupEquipmentItemViewModel(inventoryItemView);

            OpenPopup(inventoryItem);

            return inventoryItem;
        }

        private void OpenScreen(ScreenViewModel viewModel)
        {
            var uiRoot = GetUIRoot();

            uiRoot.OpenScreen(viewModel);
        }

        private void OpenPopup(ScreenViewModel viewModel)
        {
            var uiRoot = GetUIRoot();
            
            uiRoot.OpenPopup(viewModel);
        }

        private UIMainMenuRootViewModel GetUIRoot()
        {
            return Container.Build().Resolve<UIMainMenuRootViewModel>();
        }
    }
}