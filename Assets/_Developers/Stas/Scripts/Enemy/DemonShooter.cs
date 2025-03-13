using UnityEngine;
using System.Collections;
using Reflex.Attributes;
using DG.Tweening;

namespace MythicalBattles
{
    public class DemonShooter : EnemyShooter
    {
        private const int ProjectileCount = 5;

        [SerializeField] private float _rate = 2f;
        [SerializeField] private ParticleSystem _spawnPlaceMarker;

        [Inject] private ISpawnPointGenerator _spawnPointGenerator;

        Vector3[] _spawnPoints = new Vector3[ProjectileCount];

        WaitForSeconds _sleep;
        Camera _camera;

        private void Start()
        {
            _sleep = new WaitForSeconds(_rate);
            _camera = Camera.main;
        }

        protected override void Shoot()
        {
            GetSpawnPoints();

            StartCoroutine(SpawnProjecttilesCoroutine());
        }

        private void GetSpawnPoints()
        {
            for (int i = 0; i < ProjectileCount; i++)
                _spawnPoints[i] = _spawnPointGenerator.GetRandomPointOutsideRadius();
        }

        private void SpawnPlaceMarkers()
        {
            for (int i = 0; i < ProjectileCount; i++)
            {
                ParticleEffect particle = (ParticleEffect)_projectilePool.GetItem();
                particle.gameObject.SetActive(true);
                particle.gameObject.transform.parent = null;
                particle.Transform.position = _spawnPoints[i];
            }
        }

        private void SpawnProjecttiles()
        {
            for (int i = 0; i < ProjectileCount; i++)
            {
                ParticleEffect particle = (ParticleEffect)_particlePool.GetItem();
                particle.gameObject.SetActive(true);
                particle.gameObject.transform.parent = null;
                particle.Transform.position = _spawnPoints[i];
            }

            _camera.transform.DOShakePosition(0.5f, 0.5f, 15, 90, false, true);
        }

        private IEnumerator SpawnProjecttilesCoroutine()
        {
            SpawnPlaceMarkers();

            yield return _sleep;

            SpawnProjecttiles();
        }
    }
}