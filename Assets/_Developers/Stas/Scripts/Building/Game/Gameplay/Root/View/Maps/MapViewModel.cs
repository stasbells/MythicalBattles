using UnityEngine;
public class MapViewModel
{
    public Vector3Int Position => _position;

    private Vector3Int _position = new(0,0,0);
}