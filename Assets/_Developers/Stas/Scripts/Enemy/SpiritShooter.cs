using UnityEngine;

namespace MythicalBattles
{
    public class SpiritShooter : Shooter
    {
        private readonly int _projectileCount = 8;
        private readonly int _rotateAngle = 45;

        [SerializeField] private float _shotAngle;
        [SerializeField] private float _shotRotation;

        protected override void Shoot()
        {
            Vector3 rotate = _transform.eulerAngles;

            for (int i = 0; i < _projectileCount; i++)
            {
                Arrow arrow = (Arrow)_projectilePool.GetItem();
                ParticleEffect particle = (ParticleEffect)_effectPool.GetItem();

                _shootPoint.rotation = Quaternion.Euler(rotate);

                arrow.gameObject.SetActive(true);
                arrow.Transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);
                arrow.SetParticle(particle);

                arrow.Rigidbody.velocity = _shootPoint.forward * _shootSpeed;

                rotate.y += _rotateAngle;
            }
        }
    }
}