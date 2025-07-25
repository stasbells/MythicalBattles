using System.Collections;
using System.Collections.Generic;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class CompanionProjectile : ReturnableToPoolProjectile, IGetDamage
    {
        private const float DamagePart = 0.4f;
        
        [SerializeField] private float _returnToPoolDelay;
        [SerializeField] private List<ParticleSystem> _baseEffects;
        [SerializeField] private List<ParticleSystem> _collisionEffects;

        private float _damage;

        public Rigidbody Rigidbody { get; private set; }

        private void Construct()
        {
            IPlayerStats playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();

            _damage = playerStats.Damage.Value * DamagePart;
        }
        
        private void OnEnable()
        {
            PlayEffects(_baseEffects);
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.gameObject.layer == Constants.LayerEnemy)
                otherCollider.gameObject.GetComponent<Health>().TakeDamage(_damage);

            StopEffects(_baseEffects);
            
            PlayEffects(_collisionEffects);

            StartCoroutine(ReturnToPoolAfterDelay());
        }

        public float GetDamage()
        {
            return _damage;
        }

        protected override void OnAwake()
        {
            Construct();
            
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void PlayEffects(IEnumerable<ParticleSystem> effects)
        {
            foreach (ParticleSystem particleSystem in effects)
            {
                particleSystem.gameObject.SetActive(true);
                particleSystem.Play();
            }
        }
        
        private void StopEffects(IEnumerable<ParticleSystem> effects)
        {
            foreach (ParticleSystem particleSystem in effects)
            {
                particleSystem.Stop();
                particleSystem.gameObject.SetActive(false);
            }
        }

        private IEnumerator ReturnToPoolAfterDelay()
        {
            yield return new WaitForSeconds(_returnToPoolDelay);
            
            StopEffects(_collisionEffects);
            
            Pool.ReturnItem(this);
        }
    }
}
