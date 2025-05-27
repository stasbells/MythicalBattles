using ObservableCollections;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Services
{
    public class MapService
    {
        private ObservableList<LevelGeneratorViewModel> _mapViewModels = new ();

        public IObservableCollection<LevelGeneratorViewModel> MapViewModels => _mapViewModels;

        public void PlaceMap(string mapId, Vector3Int position)
        {

        }
    }
}