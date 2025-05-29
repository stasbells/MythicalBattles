using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using Reflex.Attributes;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class TestDataReseter : MonoBehaviour
    {
        private IDataProvider _dataProvider;
        private ILevelSelectionService _levelSelection;
        
        [Inject]
        private void Construct(IDataProvider dataProvider, ILevelSelectionService levelSelection)
        {
            _dataProvider = dataProvider;
            _levelSelection = levelSelection;
        }

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
