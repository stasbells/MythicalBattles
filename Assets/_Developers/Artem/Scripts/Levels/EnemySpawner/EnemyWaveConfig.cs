using UnityEngine;

namespace MythicalBattles
{
    [System.Serializable]
    public class EnemyWaveConfig
    {
        [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}
