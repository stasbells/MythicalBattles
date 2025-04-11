using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View
{
    public class WorldGameplayRootView : MonoBehaviour
    {
        [SerializeField] private Transform _worldGamplayContainer;

        public void AttachWorldGameplay(GameObject worldGameplay)
        {
            ClearWorldGameplay();

            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName != Scenes.GAMEPLAY)
                worldGameplay.transform.SetParent(_worldGamplayContainer, false);
        }

        private void ClearWorldGameplay()
        {
            var childCount = _worldGamplayContainer.childCount;

            for (var i = 0; i < childCount; i++)
                Destroy(_worldGamplayContainer.GetChild(i).gameObject);
        }
    }
}