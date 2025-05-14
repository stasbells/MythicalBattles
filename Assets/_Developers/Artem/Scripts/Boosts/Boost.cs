using System;
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

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover player))
            {
                RememberPlayer(player.transform);
                
                Apply();

                Destroy(gameObject);
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

        protected virtual void Apply()
        {
            SoundID boostKeepUpSound = _audioPlayback.AudioContainer.BoostUpKeep;
                
            _audioPlayback.Play(boostKeepUpSound);
        }
    }
}
