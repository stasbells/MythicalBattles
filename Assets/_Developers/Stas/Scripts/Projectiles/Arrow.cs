using UnityEngine;

namespace MythicalBattles
{
    public class Arrow : ReturnableProjectile
    {
        private ParticleEffect _effect;

        [field: SerializeField] public int Damage { get; private set; }

        public Rigidbody Rigidbody { get; internal set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == Constants.LayerPlayer || collision.gameObject.layer == Constants.LayerEnemy)
                collision.gameObject.GetComponent<Health>().TakeDamage(Damage);

            if (_effect != null)
            {
                _effect.ParticleSystem.Stop();
                _effect.Transform.parent = null;
                _effect = null;
            }

            _pool.ReturnItem(this);
        }

        public void SetParticle(ParticleEffect effect)
        {
            _effect = effect;
            _effect.Transform.position = Transform.position;
            _effect.Transform.parent = Transform;
            _effect.gameObject.SetActive(true);
        }
    }
}