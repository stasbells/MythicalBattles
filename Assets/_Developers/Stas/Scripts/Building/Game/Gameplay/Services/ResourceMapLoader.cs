using System.Threading.Tasks;
using UnityEngine;

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