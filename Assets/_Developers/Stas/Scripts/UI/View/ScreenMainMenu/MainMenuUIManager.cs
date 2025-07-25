using MythicalBattles.Assets._Developers.Stas.Scripts.UI.Root.MainMenu;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupB;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupEquipmentItem;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLeaderboard;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelSelector;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenSettings;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenShop;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenTutorial;
using R3;
using Reflex.Core;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu
{
    public class MainMenuUIManager : UIManager
    {
        private readonly Subject<Unit> _exitSceneRequest;
        private readonly ReactiveProperty<ShopPanel> _shopPanel = new();

        public MainMenuUIManager(ContainerBuilder builder) : base(builder)
        {
            _exitSceneRequest = builder.Build().Resolve<Subject<Unit>>();
        }

        public ScreenLevelSelectorViewModel OpenScreenLevelSelector()
        {
            var viewModel = new ScreenLevelSelectorViewModel(this, _exitSceneRequest);
            var UIRoot = Container.Build().Resolve<UIMainMenuRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenMainMenuViewModel OpenScreenMainMenu()
        {
            var viewModel = new ScreenMainMenuViewModel(this);
            var UIRoot = Container.Build().Resolve<UIMainMenuRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenShopViewModel OpenScreenShop()
        {
            var viewModel = new ScreenShopViewModel(this, _shopPanel);
            var UIRoot = Container.Build().Resolve<UIMainMenuRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenTutorialViewModel OpenScreenTutorial()
        {
            var viewModel = new ScreenTutorialViewModel(_exitSceneRequest);
            var UIRoot = Container.Build().Resolve<UIMainMenuRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenLeaderboardViewModel OpenScreenLeaderboard()
        {
            var viewModel = new ScreenLeaderboardViewModel(this);
            var UIRoot = Container.Build().Resolve<UIMainMenuRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public ScreenSettingsViewModel OpenScreenSettings()
        {
            var viewModel = new ScreenSettingsViewModel(this);
            var UIRoot = Container.Build().Resolve<UIMainMenuRootViewModel>();

            UIRoot.OpenScreen(viewModel);

            return viewModel;
        }

        public PopupShopItemViewModel OpenPopupShopItem(ShopPanel shopPanel, ShopItemView shopItemView)
        {
            var shopItem = new PopupShopItemViewModel(shopPanel, shopItemView);
            var UIRoot = Container.Build().Resolve<UIMainMenuRootViewModel>();

            UIRoot.OpenPopup(shopItem);

            return shopItem;
        }

        public PopupEquipmentItemViewModel OpenPopupEquipmentItem(InventoryItemView inventoryItemView)
        {
            var inventoryItem = new PopupEquipmentItemViewModel(inventoryItemView);
            var UIRoot = Container.Build().Resolve<UIMainMenuRootViewModel>();

            UIRoot.OpenPopup(inventoryItem);

            return inventoryItem;
        }
    }
}