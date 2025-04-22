using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View
{
    public class WorldGameplayRootBinder : MonoBehaviour
    {
        [SerializeField] private MapBinder _map;

        private MapBinder _currentMap;

        public void Bind(WorldGameplayRootViewModel viewModel)
        {
            PlaceMap(viewModel.MapViewModel);
        }

        private void PlaceMap(MapViewModel mapViewModel)
        {
            var mapBinder = Instantiate(_map, transform);
            mapBinder.Bind(mapViewModel);

            _currentMap = mapBinder;
        }
    }
}
