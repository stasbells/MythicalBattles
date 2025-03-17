using UnityEngine;

namespace MythicalBattles
{
    public abstract class Boost : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover mover))
            {
                Apply();
                
                Destroy(gameObject);  //потом возможно поменять на отключение и помещение в пул
            }
        }

        protected abstract void Apply();
    }
}
