using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MythicalBattles.Levels.EnemySpawner
{
    public class EnemySpawnPoints : MonoBehaviour
    {
       [SerializeField] private List<Transform> _spawnPoints;
       [SerializeField] private Transform _bossSpawnPoint;

       public IEnumerable<Vector3> GetSpawnPointsPositions()
       {
           return _spawnPoints
               .Where(pointTransform => pointTransform != null)
               .Select(pointTransform => pointTransform.position);
       }

       public Vector3 GetBossSpawnPointPosition()
       {
           return _bossSpawnPoint.position;
       }
    }
}
