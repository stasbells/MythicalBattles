using UnityEngine;
using System.Collections;
using Reflex.Attributes;
using DG.Tweening;

namespace MythicalBattles
{
    public class DemonShooter : Shooter
    {
        private const int ProjectileCount = 5;

        [SerializeField] private float _attackDelay = 2f;
        [SerializeField] private float _afterAttackDelay = 1f;
        [SerializeField] private ParticleSystem _spawnPlaceMarker;

        [Inject] private ISpawnPointGenerator _spawnPointGenerator;

        private Vector3[] _spawnPoints = new Vector3[ProjectileCount];

        private WaitForSeconds _projectilesSpawnDelay;
        private WaitForSeconds _animationDelay;
        private Transform _cameraTransform;

        private void Start()
        {
            _projectilesSpawnDelay = new WaitForSeconds(_attackDelay);
            _animationDelay = new WaitForSeconds(_afterAttackDelay);
            _cameraTransform = Camera.main.transform;
        }

        protected override void Shoot()
        {
            GetSpawnPoints();

            StartCoroutine(UltimateAttack());
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

        private IEnumerator UltimateAttack()
        {
            SpawnPlaceMarkers();

            yield return _projectilesSpawnDelay;

            SpawnProjecttiles();

            yield return _animationDelay;

            _animator.SetBool(Constants.IsMove, true);
            _animator.SetBool(Constants.IsAttack, false);
        }
    }
}