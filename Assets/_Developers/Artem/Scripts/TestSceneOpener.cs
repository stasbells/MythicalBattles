using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class TestSceneOpener : MonoBehaviour
    {
        private IDataProvider _dataProvider;
        
        [Inject]
        private void Construct(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        
        public void OpenArtemScene()
        {
            SceneManager.LoadScene(1);
        }
        
        public void OpenStasScene()
        {
            SceneManager.LoadScene(0);
        }

        public void ResetItems()
        {
            _dataProvider.ResetData();
        }
    }
}
