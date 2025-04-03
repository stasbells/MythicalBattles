using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Root
{
    class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _sceneRootBinder;

        public void Run()
        {
            Debug.Log("Game started");
        }
    }
}
