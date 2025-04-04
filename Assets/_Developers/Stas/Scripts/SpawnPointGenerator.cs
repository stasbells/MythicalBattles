using UnityEngine;

namespace MythicalBattles
{
    public class SpawnPointGenerator : ISpawnPointGenerator
    {
        private float _unspawnRadius = 3f;

        public Vector3 GetRandomPointOutsideRadius()
        {
            Vector3 spawnPoint = GetRandomPoint();

            while (Physics.OverlapSphere(spawnPoint, _unspawnRadius, Constants.MaskLayerEnemy).Length != 0)
                spawnPoint = GetRandomPoint();

            return spawnPoint;
        }

        public Vector3 GetRandomPoint()
        {
            return new Vector3(Random.Range(Constants.SpawnPointXMinus, Constants.SpawnPointXPlus),
                Constants.SpawnPointY, Random.Range(Constants.SpawnPointZMinus, Constants.SpawnPointZPlus));
        }
    }
}