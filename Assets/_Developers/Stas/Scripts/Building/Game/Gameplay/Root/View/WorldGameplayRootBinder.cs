using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;
using Reflex.Core;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View
{
    public class WorldGameplayRootBinder : MonoBehaviour
    {
        [SerializeField] private LevelGenerator _levelGeneratorPrefab;
        [SerializeField] private GameObject _archerPrefab;
        [SerializeField] private PlayerFollower _cameraSystemPrefab;

        public void Bind(WorldGameplayRootViewModel viewModel)
        {
            InitLevelGenerator(viewModel.GamplayContainer);
            InitCameraSystem(InitArcher());
        }

        private void InitLevelGenerator(Container gamplayContainer)
        {
            var uiManager = gamplayContainer.Resolve<GameplayUIManager>();
            var levelGeneratorBinder = Instantiate(_levelGeneratorPrefab);

            // levelGeneratorBinder.Bind(levelGeneratorViewModel);
        }

        private Transform InitArcher()
        {
            var archerInstance = Instantiate(_archerPrefab);

            return archerInstance.transform;
        }

        public void InitCameraSystem(Transform archerTransform)
        {
            var cameraInstance = Instantiate(_cameraSystemPrefab);
            cameraInstance.SetTarget(archerTransform);
        }
    }
}