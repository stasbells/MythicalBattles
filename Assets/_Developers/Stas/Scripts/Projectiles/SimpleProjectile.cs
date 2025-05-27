using Ami.BroAudio;
using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class SimpleProjectile : MonoBehaviour, IGetDamage
    {
        private float _damage;
        private IAudioPlayback _audioPlayback;

        private void Awake()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.layer == Constants.LayerPlayer || other.layer == Constants.LayerEnemy)
            {
                other.GetComponent<Health>().TakeDamage(_damage);
                
                if(other.layer == Constants.LayerEnemy && TryGetComponent(out PeriodicDamageProjectile projectile) == false)
                {
                    SoundID shotSound = _audioPlayback.AudioContainer.BaseShot;
                    
                    _audioPlayback.Play(shotSound);
                }
            }
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public float GetDamage()
        {
            return _damage;
        }
    }
}