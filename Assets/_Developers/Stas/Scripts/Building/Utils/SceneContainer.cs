using Reflex.Core;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils
{
    public static class SceneContainer
    {
        private readonly static Container _container = SceneManager.GetActiveScene().GetSceneContainer();

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}