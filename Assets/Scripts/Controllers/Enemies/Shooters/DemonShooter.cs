using Ami.BroAudio;
using DG.Tweening;
using Reflex.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using MythicalBattles.Assets.Scripts.Controllers.Projectiles.ObjectPool;
using MythicalBattles.Assets.Scripts.Controllers.Projectiles;
using MythicalBattles.Assets.Scripts.Services.AudioPlayback;
using MythicalBattles.Assets.Scripts.Utils;

namespace MythicalBattles.Assets.Scripts.Controllers.Enemies.Shooters
{
    public class DemonShooter : Shooter, IWaveDamageMultiplier
    {
        private readonly SpawnPointGenerator _spawnPointGenerator = new();

        [SerializeField] private int _projectileCount = 6;
        [SerializeField] private ProjectilesObjectPool _projectilePool;
        [SerializeField] private ProjectilesObjectPool _effectPool;
        [SerializeField] private float _attackDelay = 2f;
        [SerializeField] private float _afterAttackDelay = 1f;
        [SerializeField] private ParticleSystem _spawnPlaceMarker;

        private Vector3[] _spawnPoints;
        private WaitForSeconds _projectilesSpawnDelay;
        private WaitForSeconds _animationDelay;
        private Transform _cameraTransform;
        private Coroutine _attackCoroutine;
        private IAudioPlayback _audioPlayback;

        private void Start()
        {
            _projectilesSpawnDelay = new WaitForSeconds(_attackDelay);
            _animationDelay = new WaitForSeconds(_afterAttackDelay);
            _cameraTransform = Camera.main.transform;
            _spawnPoints = new Vector3[_projectileCount];
        }

        public void ApplyMultiplier(float multiplier)
        {
            foreach (var item in _projectilePool.Items)
            {
                if (item.TryGetComponent(out UltimateDamager damager))
                    damager.ApplyMultiplier(multiplier);
            }
        }

        public void CancelMultiplier()
        {
            foreach (var item in _projectilePool.Items)
            {
                if (item.TryGetComponent(out UltimateDamager damager))
                    damager.CancelMultiplier();
            }
        }

        protected override void OnShooterAwake()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }

        protected override void Shoot()
        {
            if (_attackCoroutine == null)
            {
                GetSpawnPoints();

                _attackCoroutine = StartCoroutine(UltimateAttack());
            }
        }

        private void GetSpawnPoints()
        {
            for (int i = 0; i < _projectileCount; i++)
                _spawnPoints[i] = _spawnPointGenerator.GetRandomPointOutsideRadius();
        }

        private void SpawnPlaceMarkers()
        {
            for (int i = 0; i < _projectileCount; i++)
            {
                ParticleEffect particle = (ParticleEffect)_effectPool.GetItem();
                particle.gameObject.SetActive(true);
                particle.Transform.parent = null;
                particle.Transform.position = _spawnPoints[i];
            }

            SoundID bossSpell = _audioPlayback.AudioContainer.BossSpell;

            _audioPlayback.PlaySound(bossSpell);
        }

        private void SpawnProjecttiles()
        {
            for (int i = 0; i < _projectileCount; i++)
            {
                ParticleEffect particle = (ParticleEffect)_projectilePool.GetItem();
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

            Animator.SetBool(Constants.IsMove, true);
            Animator.SetBool(Constants.IsShoot, false);

            _attackCoroutine = null;
        }
    }
}