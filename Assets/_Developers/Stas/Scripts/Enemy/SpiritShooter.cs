using UnityEngine;

namespace MythicalBattles
{
    public class SpiritShooter : EnemyShooter
    {
        private readonly int _projecttileCount = 8;
        private readonly int _rotateAngle = 45;

        [SerializeField] private float _shotAngle;
        [SerializeField] private float _shotRotation;

        protected override void Shoot()
        {
            Vector3 rotate = _transform.eulerAngles;

            for (int i = 0; i < _projecttileCount; i++)
            {
                Arrow arrow = (Arrow)_projectilePool.GetItem();
                ParticleEffect particle = (ParticleEffect)_particlePool.GetItem();

                _shootPoint.rotation = Quaternion.Euler(rotate);

                arrow.gameObject.SetActive(true);
                arrow.transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);
                arrow.SetParticle(particle);

                Rigidbody rigidbody = arrow.GetComponent<Rigidbody>();
                rigidbody.velocity = _shootPoint.forward * _shootSpeed;

                rotate.y += _rotateAngle;
            }
        }
    }
}