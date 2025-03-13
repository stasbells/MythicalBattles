using UnityEngine;

namespace MythicalBattles
{
    public class Arrow : Projectile
    {
        private ParticleEffect _effect;

        [field: SerializeField] public int Damage { get; private set; }

        public Rigidbody Rigidbody { get; internal set; }
        public Transform Transform { get; internal set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = GetComponent<Transform>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == Constants.LayerPlayer || collision.gameObject.layer == Constants.LayerEnemy)
                collision.gameObject.GetComponent<Health>().TakeDamage(Damage);

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
        }
    }
}