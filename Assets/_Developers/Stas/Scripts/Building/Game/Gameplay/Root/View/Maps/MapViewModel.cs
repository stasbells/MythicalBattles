using System;
using UnityEngine;

public class MapViewModel : IDisposable
{
    private readonly MapModel _model;
    private readonly IMapLoader _mapLoader;

    public event Action<GameObject> OnMapLoaded;
    public event Action OnMapUnloaded;
    public event Action<string> OnError;

    public string CurrentMapName => _model.MapName;
    public bool IsMapLoaded { get; private set; }

    public MapViewModel(MapModel model, IMapLoader mapLoader)
    {
        _model = model;
        _mapLoader = mapLoader;

        _model.OnMapLoaded += HandleMapLoaded;
        _model.OnMapUnloaded += HandleMapUnloaded;
    }

    public async void LoadMapAsync()
    {
        try
        {
            var mapPrefab = await _mapLoader.LoadMapPrefabAsync(_model.MapName);
            if (mapPrefab != null)
            {
                _model.LoadMap(mapPrefab);
            }
            else
            {
                OnError?.Invoke($"Failed to load map: {_model.MapName}");
            }
        }
        catch (Exception ex)
        {
            OnError?.Invoke($"Error loading map: {ex.Message}");
        }
    }

    public void UnloadMap()
    {
        _model.UnloadMap();
    }

    private void HandleMapLoaded(GameObject map)
    {
        IsMapLoaded = true;
        OnMapLoaded?.Invoke(map);
    }

    private void HandleMapUnloaded()
    {
        IsMapLoaded = false;
        OnMapUnloaded?.Invoke();
    }

    public void Dispose()
    {
        _model.OnMapLoaded -= HandleMapLoaded;
        _model.OnMapUnloaded -= HandleMapUnloaded;
    }
}