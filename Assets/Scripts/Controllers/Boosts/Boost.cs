using System;
using Ami.BroAudio;
using MythicalBattles.Assets.Scripts.Controllers.Player;
using MythicalBattles.Assets.Scripts.Services.AudioPlayback;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets.Scripts.Controllers.Boosts
{
    public abstract class Boost : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _boostTakingEffect;

        private IAudioPlayback _audioPlayback;
        public event Action<Boost> Applied; 
        protected Transform Player { get; private set; }

        private void Construct()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }
        
        private void Awake()
        {
           Construct(); 
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
            Applied?.Invoke(this);

            SoundID boostKeepUpSound = _audioPlayback.AudioContainer.BoostUpKeep;
                
            _audioPlayback.PlaySound(boostKeepUpSound);
        }
        
        private void RememberPlayer(Transform player)
        {
            Player = player;
        }
    }
}
