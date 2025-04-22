using UnityEngine;

public class MapBinder : MonoBehaviour
{
    public void Bind(MapViewModel mapViewModel)
    {
        transform.position = mapViewModel.Position;
    }
}