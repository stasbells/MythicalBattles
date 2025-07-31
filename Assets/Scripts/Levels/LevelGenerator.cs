using System;
using Ami.BroAudio;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;
using MythicalBattles.Levels.EnemySpawner;
using MythicalBattles.Services.AudioPlayback;
using MythicalBattles.Services.LevelSelection;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Levels
{
    [RequireComponent(typeof(LevelEndAlgorithm))]
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelConfig[] _levelConfigs;
        [SerializeField] private int _timeBetweenWaves = 6;
        [SerializeField] private float _enemyDyingTime = 3f;

        private ILevelSelectionService _levelSelection;
        private IAudioPlayback _audioPlayback;
        private LevelEndAlgorithm _levelEndAlgorithm;
        private WavesSpawner _spawner;
        private int _currentLevelNumber;
        private Canvas _canvas;
        
        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _levelSelection = container.Resolve<ILevelSelectionService>();
            _audioPlayback = container.Resolve<IAudioPlayback>();
        }

        private void Awake()
        {
            Construct();

            _canvas = GetComponentInParent<WorldGameplayRootBinder>().GameplayContainer
                .Resolve<UIRootView>().GetComponentInChildren<Canvas>();

            _levelEndAlgorithm = GetComponent<LevelEndAlgorithm>();

            InitializeLevel();
        }

        private void OnDisable()
        {
            if (_spawner != null)
                _spawner.AllWavesCompleted -= OnAllWavesCompleted;
        }

        private void InitializeLevel()
        {
            _currentLevelNumber = _levelSelection.CurrentLevelNumber;

            if (_currentLevelNumber < 0 || _currentLevelNumber > _levelConfigs.Length)
                throw new InvalidOperationException();

            SpawnLevelDesign(_currentLevelNumber);

            InitializeWaveSpawner(_currentLevelNumber);

            PlayLevelMusicTheme(_currentLevelNumber);
        }

        private void SpawnLevelDesign(int levelNumber)
        {
            var designPrefab = _levelConfigs[levelNumber - 1].LevelDesignPrefab;

            if (designPrefab == null)
                throw new InvalidOperationException();

            GameObject map = Instantiate(designPrefab);

            StaticBatchingUtility.Combine(map);
        }

        private void InitializeWaveSpawner(int levelNumber)
        {
            var spawnerPrefab = _levelConfigs[levelNumber - 1].WavesSpawner;

            if (spawnerPrefab == null)
                throw new InvalidOperationException();

            _spawner = Instantiate(spawnerPrefab);

            if (_spawner.TryGetComponent(out WaveProgressHandler waveProgressHandler) == false)
                throw new InvalidOperationException();

            waveProgressHandler.Initialize(_canvas, _spawner.WavesCount, _timeBetweenWaves);

            _spawner.SetTimeBetweenWaves(_timeBetweenWaves);

            _spawner.SetEnemiesDyingTime(_enemyDyingTime);

            _spawner.AllWavesCompleted += OnAllWavesCompleted;
        }

        private void PlayLevelMusicTheme(int levelNumber)
        {
            SoundID musicTheme = _levelConfigs[levelNumber - 1].MusicTheme;

            _audioPlayback.PlayMusic(musicTheme);
        }

        private void OnAllWavesCompleted()
        {
            float currentLevelBaseReward = _levelConfigs[_currentLevelNumber - 1].BaseRewardMoney;

            float currentLevelMaxScore = _levelConfigs[_currentLevelNumber - 1].MaxScore;

            StartCoroutine(_levelEndAlgorithm.Run(_currentLevelNumber, currentLevelBaseReward, currentLevelMaxScore));
        }

        public void SetUiManager(GameplayUIManager gameplayUIManager)
        {
            _levelEndAlgorithm.SetUiManager(gameplayUIManager);
        }
    }
}
