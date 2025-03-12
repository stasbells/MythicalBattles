using UnityEngine;
using System.Collections;
using Reflex.Attributes;

namespace MythicalBattles
{
    public class DemonShooter : EnemyShooter
    {
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private float _maxDistance = 10f;

        [Inject] private ISpawnPointGenerator _spawnPointGenerator;

        private int _projectileCount = 5;

        protected override void Shoot()
        {
            for (int i = 0; i < _projectileCount; i++)
            {
                ParticleEffect particle = (ParticleEffect)_particlePool.GetItem();

                particle.gameObject.SetActive(true);
                particle.transform.position = _spawnPointGenerator.GetRandomPointOutsideRadius();
            }

            //StartCoroutine(OnSoot());
        }

        private IEnumerator OnSoot()
        {
            float distanceTraveled = 0f;
            Vector3 direction = -transform.up;

            while (distanceTraveled < _maxDistance)
            {
                if (Physics.SphereCast(transform.position, _radius, direction, out RaycastHit hit, _shootSpeed * Time.deltaTime, Constants.LayerPlayer))
                {
                    Debug.Log(hit.collider.gameObject);
                    yield break;
                }

                distanceTraveled += _shootSpeed * Time.deltaTime;
                transform.position += direction * _shootSpeed * Time.deltaTime;

                yield return null;
            }
        }
    }
}
