using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapRootBinder : MonoBehaviour
{
    [SerializeField] private TMP_Text _mapNameText;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _unloadButton;
    [SerializeField] private TMP_Text _statusText;
    [SerializeField] private Transform _mapContainer;

    private MapViewModel _viewModel;

    public void Initialize(MapViewModel viewModel)
    {
        _viewModel = viewModel;

        _loadButton.onClick.AddListener(OnLoadButtonClicked);
        _unloadButton.onClick.AddListener(OnUnloadButtonClicked);

        _viewModel.OnMapLoaded += HandleMapLoaded;
        _viewModel.OnMapUnloaded += HandleMapUnloaded;
        _viewModel.OnError += HandleError;

        UpdateUI();
    }

    private void OnDestroy()
    {
        if (_viewModel != null)
        {
            _loadButton.onClick.RemoveListener(OnLoadButtonClicked);
            _unloadButton.onClick.RemoveListener(OnUnloadButtonClicked);

            _viewModel.OnMapLoaded -= HandleMapLoaded;
            _viewModel.OnMapUnloaded -= HandleMapUnloaded;
            _viewModel.OnError -= HandleError;

            _viewModel.Dispose();
        }
    }

    private void OnLoadButtonClicked()
    {
        _viewModel.LoadMapAsync();
    }

    private void OnUnloadButtonClicked()
    {
        _viewModel.UnloadMap();
    }

    private void HandleMapLoaded(GameObject map)
    {
        map.transform.SetParent(_mapContainer, false);
        UpdateUI();
        _statusText.text = $"Map loaded: {_viewModel.CurrentMapName}";
    }

    private void HandleMapUnloaded()
    {
        UpdateUI();
        _statusText.text = "Map unloaded";
    }

    private void HandleError(string errorMessage)
    {
        _statusText.text = $"Error: {errorMessage}";
        Debug.LogError(errorMessage);
    }

    private void UpdateUI()
    {
        _mapNameText.text = _viewModel.CurrentMapName;
        _loadButton.interactable = !_viewModel.IsMapLoaded;
        _unloadButton.interactable = _viewModel.IsMapLoaded;
    }
}