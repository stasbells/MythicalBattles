using UnityEngine;
using System.Threading.Tasks;

public interface IMapLoader
{
    Task<GameObject> LoadMapPrefabAsync(string mapName);
}