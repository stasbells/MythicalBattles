using UnityEngine;
using System.Threading.Tasks;

public class ResourceMapLoader : IMapLoader
{
    public async Task<GameObject> LoadMapPrefabAsync(string mapName)
    {
        var request = Resources.LoadAsync<GameObject>($"Maps/{mapName}");

        while (!request.isDone)
        {
            await Task.Yield();
        }

        return request.asset as GameObject;
    }
}