using System;
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

        private Container _mainMenuContainer;
        private IPersistentData _persistentData;
        private IDataProvider _dataProvider;
        private IAudioPlayback _audioPlayback;

        private void Construct()
        {
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
            _dataProvider = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IDataProvider>();
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }
        
        private void Awake()
        {
            Construct();
            
            LoadData();
        }
        
        public Observable<Unit> Run(Container mainMenuContainer)
        {
            _mainMenuContainer = new ContainerBuilder().SetParent(mainMenuContainer)
                .AddSingleton(new Subject<Unit>())
                .Build();

            var mainMenuViewModelsContainer = new ContainerBuilder().SetParent(mainMenuContainer);

            mainMenuViewModelsContainer
                .AddSingleton(new Subject<Unit>())
                .AddSingleton(new MainMenuUIManager(mainMenuViewModelsContainer))
                .AddSingleton(typeof(UIMainMenuRootViewModel))
                .Build();

            InitUI(mainMenuViewModelsContainer.Build());
            
            PlayMenuTheme();

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

        private void PlayMenuTheme()
        {
            SoundID mainThemeID = _audioPlayback.AudioContainer.MenuTheme;
            
            _audioPlayback.PlayMusic(mainThemeID);
        }
    }
}