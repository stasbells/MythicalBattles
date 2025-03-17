using UnityEngine;

namespace MythicalBattles
{
    public interface ISpawnPointGenerator
    {
        Vector3 GetRandomPointOutsideRadius();
        Vector3 GetRandomPoint();
    }
}