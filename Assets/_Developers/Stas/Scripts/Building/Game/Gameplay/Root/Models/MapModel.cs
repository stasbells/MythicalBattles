using UnityEngine;

public class MapModel
{
    public string MapName { get; private set; }

    public event System.Action<GameObject> OnMapLoaded;
    public event System.Action OnMapUnloaded;

    private GameObject _loadedMap;

    public MapModel(string mapName)
    {
        MapName = mapName;
    }

    public void LoadMap(GameObject mapPrefab)
    {
        if (_loadedMap != null)
            UnloadMap();
        
        _loadedMap = Object.Instantiate(mapPrefab);
        _loadedMap.name = MapName;

        OnMapLoaded?.Invoke(_loadedMap);
    }

    public void UnloadMap()
    {
        if (_loadedMap != null)
        {
            Object.Destroy(_loadedMap);
            _loadedMap = null;
            OnMapUnloaded?.Invoke();
        }
    }
}