using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class CompanionProjectile : ReturnableToPoolProjectile, IGetDamage
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _returnToPoolDelay;
        [SerializeField] private List<ParticleSystem> _baseEffects;
        [SerializeField] private List<ParticleSystem> _collisionEffects;

        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            _transform = transform;
            Rigidbody = GetComponent<Rigidbody>();
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
            
            _pool.ReturnItem(this);
        }

        public float GetDamage()
        {
            return _damage;
        }
    }
}
