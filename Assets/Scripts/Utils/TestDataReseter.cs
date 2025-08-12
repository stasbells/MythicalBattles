using MythicalBattles.Assets.Scripts.Services.Data;
using MythicalBattles.Assets.Scripts.Services.LevelSelection;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets.Scripts.Utils
{
    public class TestDataReseter : MonoBehaviour
    {
        private IDataProvider _dataProvider;
        private ILevelSelectionService _levelSelection;

        private void Awake()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _dataProvider = container.Resolve<IDataProvider>();
            _levelSelection = container.Resolve<ILevelSelectionService>();
        }

        public void ResetItems()
        {
            _dataProvider.ResetPlayerData();
        }

        public void ResetProgressData()
        {
            _dataProvider.ResetProgressData();
        }
    }
}