using Ami.BroAudio;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.Root.MainMenu;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu;
using R3;
using Reflex.Core;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.MainMenu
{
    class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

        private IDataProvider _dataProvider;
        private IAudioPlayback _audioPlayback;

        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _dataProvider = container.Resolve<IDataProvider>();
            _audioPlayback = container.Resolve<IAudioPlayback>();
        }

        private void Awake()
        {
            Construct();

            LoadData();
        }

        public Observable<Unit> Run(Container mainMenuContainer)
        {
            var mainMenuViewModelsContainer = new ContainerBuilder().SetParent(mainMenuContainer);

            mainMenuViewModelsContainer
                .AddSingleton(new Subject<Unit>())
                .AddSingleton(new MainMenuUIManager(mainMenuViewModelsContainer))
                .AddSingleton(typeof(UIMainMenuRootViewModel))
                .Build();

            InitUI(mainMenuViewModelsContainer.Build());

            PlayLevelTheme();

            var exitSceneSignal = mainMenuViewModelsContainer.Build().Resolve<Subject<Unit>>();

            return exitSceneSignal.AsObservable();
        }

        private void LoadData()
        {
            _dataProvider.LoadPlayerData();
            _dataProvider.LoadGameProgressData();
            _dataProvider.LoadSettingsData();
        }

        private void InitUI(Container viewsContainer)
        {
            var uiRoot = viewsContainer.Resolve<UIRootView>();
            var uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

            var uiSceneRootViewModel = viewsContainer.Resolve<UIMainMenuRootViewModel>();
            uiSceneRootBinder.Bind(uiSceneRootViewModel);

            var uiManager = viewsContainer.Resolve<MainMenuUIManager>();
            uiManager.OpenScreenShop();
            uiManager.OpenScreenMainMenu();
        }

        private void PlayLevelTheme()
        {
            SoundID mainThemeID = _audioPlayback.AudioContainer.MenuTheme;

            _audioPlayback.PlayMusic(mainThemeID);
        }
    }
}