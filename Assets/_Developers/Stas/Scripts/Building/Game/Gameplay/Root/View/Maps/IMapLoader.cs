using System.Threading.Tasks;
using UnityEngine;

public interface IMapLoader
{
    Task<GameObject> LoadMapPrefabAsync(string mapName);
}