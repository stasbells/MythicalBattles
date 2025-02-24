using UnityEngine;

namespace MythicalBattles
{
    public class Projectile : MonoBehaviour
    {
        private protected ObjectPool _pool;

        public void SetPool(ObjectPool pool) { _pool = pool; }
    }
}