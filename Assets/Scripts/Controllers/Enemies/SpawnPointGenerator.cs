using MythicalBattles.Assets.Scripts.Utils;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies
{
    public class SpawnPointGenerator
    {
        private readonly float _unspawnRadius = 3f;
        private readonly Collider[] _overlapResults = new Collider[10];
        private float _spawnPointX;
        private float _spawnPointZ;

        public Vector3 GetRandomPointOutsideRadius()
        {
            Vector3 spawnPoint = GetRandomPoint();

            while (Physics.OverlapSphereNonAlloc(spawnPoint, _unspawnRadius, _overlapResults, Constants.MaskLayerEnemy) != 0)
                spawnPoint = GetRandomPoint();

            return spawnPoint;
        }

        private Vector3 GetRandomPoint()
        {
            _spawnPointX = Random.Range(Constants.SpawnPointXMinus, Constants.SpawnPointXPlus);
            
            _spawnPointZ = Random.Range(Constants.SpawnPointZMinus, Constants.SpawnPointZPlus);
            
            return new Vector3(_spawnPointX, Constants.SpawnPointY, _spawnPointZ);
        }
    }
}