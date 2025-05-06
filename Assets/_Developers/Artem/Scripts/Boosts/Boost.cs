using UnityEngine;

namespace MythicalBattles
{
    public abstract class Boost : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _boostTakingEffect;

        protected Transform Player { get; private set; }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover player))
            {
                RememberPlayer(player.transform);
                
                Apply();

                Destroy(gameObject);  //потом возможно поменять на отключение и помещение в пул
            }
        }

        private void OnDestroy()
        {
            Instantiate(_boostTakingEffect, Player);
        }

        protected void RememberPlayer(Transform player)
        {
            Player = player;
        }

        protected abstract void Apply();
    }
}
