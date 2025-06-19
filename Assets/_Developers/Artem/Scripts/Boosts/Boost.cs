using Ami.BroAudio;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public abstract class Boost : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _boostTakingEffect;

        private IAudioPlayback _audioPlayback;
        protected Transform Player { get; private set; }

        private void Awake()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            OnTriggerEnterBehaviour(otherCollider);
            
            if (otherCollider.TryGetComponent(out PlayerMover player))
            {
                RememberPlayer(player.transform);
                
                Apply();
                
                Instantiate(_boostTakingEffect, Player);

                Destroy(gameObject);
            }
        }

        protected virtual void OnTriggerEnterBehaviour(Collider otherCollider)
        {
        }
        
        protected virtual void Apply()
        {
            SoundID boostKeepUpSound = _audioPlayback.AudioContainer.BoostUpKeep;
                
            _audioPlayback.PlaySound(boostKeepUpSound);
        }
        
        private void RememberPlayer(Transform player)
        {
            Player = player;
        }
    }
}
