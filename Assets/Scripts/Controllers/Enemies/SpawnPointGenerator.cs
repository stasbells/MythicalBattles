using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies
{
    public class SpawnPointGenerator
    {
        private readonly float _unspawnRadius = 3f;
        private readonly Collider[] _overlapResults = new Collider[10];

        public Vector3 GetRandomPointOutsideRadius()
        {
            Vector3 spawnPoint = GetRandomPoint();

            while (Physics.OverlapSphereNonAlloc(spawnPoint, _unspawnRadius, _overlapResults, Constants.MaskLayerEnemy) != 0)
                spawnPoint = GetRandomPoint();

            return spawnPoint;
        }

        private Vector3 GetRandomPoint()
        {
            return new Vector3(Random.Range(Constants.SpawnPointXMinus, Constants.SpawnPointXPlus),
                Constants.SpawnPointY, Random.Range(Constants.SpawnPointZMinus, Constants.SpawnPointZPlus));
        }
    }
}