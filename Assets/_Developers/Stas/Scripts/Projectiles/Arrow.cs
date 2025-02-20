using UnityEngine;

namespace MythicalBattles
{
    public class Arrow : Projectile
    {
        private readonly int _playerLayer = 7;

        private ParticleEffect _effect;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != _playerLayer)
            {
                _effect.gameObject.GetComponent<ParticleSystem>().Stop();
                _effect.gameObject.transform.parent = null;
                _effect = null;

                _pool.ReturnItem(this);
            }
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