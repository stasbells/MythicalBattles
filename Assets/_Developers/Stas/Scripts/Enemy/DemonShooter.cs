using UnityEngine;
using System.Collections;
using Reflex.Attributes;
using DG.Tweening;

namespace MythicalBattles
{
    public class DemonShooter : Shooter
    {
        private const int ProjectileCount = 5;

        [SerializeField] private float _rate = 2f;
        //[SerializeField] private float _delay = 0.5f;
        [SerializeField] private ParticleSystem _spawnPlaceMarker;

        [Inject] private ISpawnPointGenerator _spawnPointGenerator;

        private Vector3[] _spawnPoints = new Vector3[ProjectileCount];

        private WaitForSeconds _sleep;
        private WaitForSeconds _delay;
        private Transform _cameraTransform;

        private void Start()
        {
            _sleep = new WaitForSeconds(_rate);
            _delay = new WaitForSeconds(1f);
            _cameraTransform = Camera.main.transform;
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
                particle.Transform.parent = null;
                particle.Transform.position = _spawnPoints[i];
            }
        }

        private void SpawnProjecttiles()
        {
            for (int i = 0; i < ProjectileCount; i++)
            {
                ParticleEffect particle = (ParticleEffect)_effectPool.GetItem();
                particle.gameObject.SetActive(true);
                particle.Transform.parent = null;
                particle.Transform.position = _spawnPoints[i];
            }

            _cameraTransform.DOShakePosition(0.5f, 0.5f, 15, 90, false, true);
        }

        private IEnumerator SpawnProjecttilesCoroutine()
        {
            SpawnPlaceMarkers();

            yield return _sleep;

            SpawnProjecttiles();

            yield return _delay;

            _animator.SetBool(Constants.IsMove, true);
            _animator.SetBool(Constants.IsAttack, false);
        }
    }
}