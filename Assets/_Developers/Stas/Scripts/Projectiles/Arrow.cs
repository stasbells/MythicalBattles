using UnityEngine;

namespace MythicalBattles
{
    public class Arrow : Projectile
    {
        private ParticleEffect _effect;

        private void OnCollisionEnter(Collision collision)
        {
            if (_effect != null)
            {
                _effect.GetComponent<ParticleSystem>().Stop();
                _effect.transform.parent = null;
                _effect = null;
            }

            _pool.ReturnItem(this);
        }

        public void SetParticle(ParticleEffect effect)
        {
            _effect = effect;
            _effect.gameObject.transform.position = transform.position;
            _effect.transform.parent = transform;
            _effect.gameObject.SetActive(true);
            _effect.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}