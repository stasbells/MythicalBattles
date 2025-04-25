using UnityEngine;

namespace MythicalBattles
{
    public abstract class Boost : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _boostTakingEffect;

        private Transform _player;
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover player))
            {
                RememberPlayerTransform(player.transform);
                
                Apply();

                Destroy(gameObject);  //потом возможно поменять на отключение и помещение в пул
            }
        }

        private void OnDestroy()
        {
            Instantiate(_boostTakingEffect, _player);
        }

        protected void RememberPlayerTransform(Transform player)
        {
            _player = player.transform;
        }

        protected abstract void Apply();
    }
}
