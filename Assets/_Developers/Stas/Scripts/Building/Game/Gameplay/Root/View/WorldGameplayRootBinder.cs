using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;
using R3;
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

        public void Bind(WorldGameplayRootViewModel viewModel)
        {
            InitLevelGenerator(viewModel.GamplayContainer);
            InitCameraSystem(InitArcher());
        }

        private void InitLevelGenerator(Container gamplayContainer)
        {
            _uiManager = gamplayContainer.Resolve<GameplayUIManager>();
            
            var levelGenerator = Instantiate(_levelGeneratorPrefab);
            
            levelGenerator.SetUiManager(_uiManager);

            // levelGeneratorBinder.Bind(levelGeneratorViewModel);
        }

        private Transform InitArcher()
        {
            var archer = Instantiate(_archerPrefab);
            
            _uiManager.SubscribeOnPlayerDeath(archer.IsDead);

            return archer.transform;
        }

        private void InitCameraSystem(Transform archerTransform)
        {
            var cameraInstance = Instantiate(_cameraSystemPrefab);
            cameraInstance.SetTarget(archerTransform);
        }
    }
}