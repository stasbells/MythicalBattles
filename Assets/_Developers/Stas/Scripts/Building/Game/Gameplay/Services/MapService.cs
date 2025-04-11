using ObservableCollections;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Services
{
    public class MapService
    {
        private ObservableList<MapViewModel> _mapViewModels = new ();

        public IObservableCollection<MapViewModel> MapViewModels => _mapViewModels;

        public void PlaceMap(string mapId, Vector3Int position)
        {

        }
    }
}