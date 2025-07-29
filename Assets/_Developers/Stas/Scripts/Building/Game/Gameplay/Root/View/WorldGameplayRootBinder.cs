using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;
using Reflex.Core;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View
{
    public class WorldGameplayRootBinder : MonoBehaviour
    {
        [SerializeField] private LevelGenerator _levelGeneratorPrefab;
        [SerializeField] private PlayerHealth _archerPrefab;
        [SerializeField] private PlayerFollower _cameraSystemPrefab;

        private GameplayUIManager _uiManager;

        public Container GameplayContainer { get; private set; }

        public void Bind(WorldGameplayRootViewModel viewModel)
        {
            GameplayContainer = viewModel.GamplayContainer;

            InitLevelGenerator(GameplayContainer);
            InitCameraSystem(InitArcher());
        }

        public void InitCameraSystem(Transform archerTransform)
        {
            var cameraInstance = Instantiate(_cameraSystemPrefab);
            cameraInstance.SetTarget(archerTransform);
        }

        private void InitLevelGenerator(Container gamplayContainer)
        {
            _uiManager = gamplayContainer.Resolve<GameplayUIManager>();
            var levelGenerator = Instantiate(_levelGeneratorPrefab, transform);

            levelGenerator.SetUiManager(_uiManager);

            levelGenerator.gameObject.transform.parent = null;
        }

        private Transform InitArcher()
        {
            var archerInstance = Instantiate(_archerPrefab);

            _uiManager.SubscribeOnPlayerDeath(archerInstance.IsDead);

            return archerInstance.transform;
        }
    }
}