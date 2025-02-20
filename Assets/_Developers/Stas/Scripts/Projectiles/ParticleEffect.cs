using UnityEngine;

namespace MythicalBattles
{
    public class ParticleEffect : Projectile
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (_transform.parent == null)
                _transform.parent = _pool.transform;
        }

        private void OnDisable()
        {
            if (_transform.parent == null)
                _pool.ReturnItem(this);
        }
    }
}