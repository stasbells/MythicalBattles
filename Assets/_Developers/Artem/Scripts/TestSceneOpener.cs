using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using Reflex.Attributes;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class TestSceneOpener : MonoBehaviour
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

        public void OpenArtemScene()
        {
            SceneManager.LoadScene(Scenes.ARTEM_GAMEPLAY);
        }
        
        public void OpenStasScene()
        {
            SceneManager.LoadScene(Scenes.GAMEPLAY);
        }

        public void ResetItems()
        {
            _dataProvider.ResetPlayerData();
        }
    }
}
