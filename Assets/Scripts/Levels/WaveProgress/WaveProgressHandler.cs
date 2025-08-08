using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MythicalBattles.Assets.Scripts.Controllers.Boosts;
using MythicalBattles.Assets.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets.Scripts.Levels.WaveProgress
{
    public class WaveProgressHandler : MonoBehaviour
    {
        private const string NextWaveTextFormat = "Next wave in";
        private const float BoostDescriptionDisplayDuration = 2f;
        private const float SliderSmoothDuration = 2f;
        
        [SerializeField] private WaveProgressView _waveProgressView;
        [SerializeField] private float _sliderSmoothSpeed = 2f;
        [SerializeField] private float _fadeDuration = 0.4f;
        
        private Canvas _canvas;
        private CanvasGroup _progressSliderCanvasGroup;
        private GameObject _progressSliderObject;
        private Slider _waveProgressSlider;
        private TMP_Text _waveNumberText;
        private TMP_Text _nextWaveText;
        private TMP_Text _boostsDescriptionText;
        private BoostsDescription _boostsDescription;
        private BetweenWavesTimer _betweenWavesTimer;
        private Coroutine _smoothProgressCoroutine;
        private ParticleSystem _sliderEffect;
        private RectTransform _fillRect;
        private int _currentWaveTotalEnemies;
        private int _defeatedEnemies;
        private int _wavesCount;
        private int _currentWaveNumber;
        private int _timeBetweenWaves;
        private Queue<Boost> _boostQueue = new ();
        private bool _isDisplayingBoost;
        
        public void Initialize(Canvas canvas, int wavesCount, int timeBetweenWaves)
        {
            _canvas = canvas;
            _wavesCount = wavesCount;
            _timeBetweenWaves = timeBetweenWaves;
            
            _betweenWavesTimer = new BetweenWavesTimer(_timeBetweenWaves);

            WaveProgressView progressSliderView = InstantiateWaveProgressView();

            HashVariables(progressSliderView);
            
            _progressSliderObject.SetActive(false);
            _nextWaveText.gameObject.SetActive(false);
            _boostsDescriptionText.gameObject.SetActive(false);
        }
        
        public void InitializeWave(int totalEnemies, int waveNumber)
        {
            if (totalEnemies <= 0)
                throw new InvalidOperationException();

            _currentWaveNumber = waveNumber;
            
            ShowProgressBar(totalEnemies);
        }

        public void OnEnemyDefeated()
        {
            _defeatedEnemies++;
            
            UpdateProgressSmoothly();
        }

        public void SubscribeOnBoostTaking(Boost boost)
        {
            boost.Applied += OnBoostApplied;
        }

        private WaveProgressView InstantiateWaveProgressView()
        {
            WaveProgressView progressSliderView = Instantiate(_waveProgressView, _canvas.transform);
            
            progressSliderView.transform.SetAsFirstSibling();
            
            return progressSliderView;
        }

        private void HashVariables(WaveProgressView progressSliderView)
        {
            _progressSliderObject = progressSliderView.ProgressBar;
            _nextWaveText = progressSliderView.NextWaveText;
            _boostsDescription = progressSliderView.BoostsDescription;
            
            if (_boostsDescription.TryGetComponent(out TMP_Text text) == false)
                throw new InvalidOperationException();
                
            _boostsDescriptionText = text;

            if (_progressSliderObject.TryGetComponent(out CanvasGroup canvasGroup) == false)
                throw new InvalidOperationException();

            _progressSliderCanvasGroup = canvasGroup;
            
            _waveProgressSlider = _progressSliderObject.GetComponentInChildren<Slider>();
            
            _waveNumberText = _progressSliderObject.GetComponentInChildren<TMP_Text>();

            _progressSliderCanvasGroup.alpha = 0f;
        }

        private void ShowProgressBar(int totalEnemies)
        {
            FadeIn(_progressSliderCanvasGroup);

            _waveNumberText.text = $"{_currentWaveNumber}";
            _currentWaveTotalEnemies = totalEnemies;
            _defeatedEnemies = 0;
        
            if (_waveProgressSlider != null)
            {
                _waveProgressSlider.value = 0f;
                
                if (_smoothProgressCoroutine != null)
                    StopCoroutine(_smoothProgressCoroutine);
            }
        }

        private void OnBoostApplied(Boost boost)
        {
            _boostQueue.Enqueue(boost);

            boost.Applied -= OnBoostApplied;
            
            if (_isDisplayingBoost == false)
                DisplayNextBoost();
        }

        private void DisplayNextBoost()
        {
            if (_boostQueue.Count == 0)
            {
                _isDisplayingBoost = false;
                return;
            }

            _isDisplayingBoost = true;
            
            Boost nextBoost = _boostQueue.Dequeue();
            
            FadeIn(_boostsDescriptionText);
            
            _boostsDescription.Display(nextBoost);

            StartCoroutine(FadeOutBoostDescription());
        }
        
        private IEnumerator FadeOutBoostDescription()
        {
            yield return new WaitForSeconds(BoostDescriptionDisplayDuration);
            
            FadeOut(_boostsDescriptionText);
        }

        private void UpdateProgressSmoothly()
        {
            float targetProgress = (float)_defeatedEnemies / _currentWaveTotalEnemies;
        
            if (_smoothProgressCoroutine != null)
                StopCoroutine(_smoothProgressCoroutine);
        
            _smoothProgressCoroutine = StartCoroutine(SmoothProgressUpdate(targetProgress));
        }

        private IEnumerator SmoothProgressUpdate(float targetProgress)
        {
            float startValue = _waveProgressSlider.value;
            float elapsed = 0f;

            while (elapsed < SliderSmoothDuration)
            {
                float currentProgress = Mathf.Lerp(startValue, targetProgress, elapsed);
                
                _waveProgressSlider.value = currentProgress;

                elapsed += Time.deltaTime * _sliderSmoothSpeed;
                
                yield return null;
            }

            _waveProgressSlider.value = targetProgress;

            if (_defeatedEnemies == _currentWaveTotalEnemies)
            {
                FadeOut(_progressSliderCanvasGroup);
                
                yield return new WaitForSeconds(_fadeDuration);

                if (_currentWaveNumber != _wavesCount)
                {
                    FadeIn(_nextWaveText);

                    _betweenWavesTimer.Start();
                    
                    _betweenWavesTimer.Ticked += OnTimerBetweenWavesTicked;
                    
                    _betweenWavesTimer.Elapsed += OnTimerBetweenWavesElapsed;
                }
            }
        }
        
        private void OnTimerBetweenWavesTicked(int ticks)
        {
            _waveProgressView.NextWaveText.text = $"{LanguagesDictionary.GetTranslation(NextWaveTextFormat)} {ticks}";
        }

        private void OnTimerBetweenWavesElapsed()
        {
            _betweenWavesTimer.Ticked -= OnTimerBetweenWavesTicked;
            
            _betweenWavesTimer.Elapsed -= OnTimerBetweenWavesElapsed;
            
            StartCoroutine(WaitForNextWaveTextFadeOut());
        }

        private IEnumerator WaitForNextWaveTextFadeOut()
        {
            yield return new WaitForSeconds(_fadeDuration);
            
            FadeOut(_nextWaveText);
        }

        private void FadeIn(Component target)
        {
            target.gameObject.SetActive(true);
            
            switch (target)
            {
                case CanvasGroup canvasGroup:
                    canvasGroup.DOFade(1, _fadeDuration);
                    break;
                case Graphic graphic:
                    graphic.DOFade(1, _fadeDuration);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
        
        private void FadeOut(Component target)
        {
            switch (target)
            {
                case CanvasGroup canvasGroup:
                    canvasGroup.DOFade(0, _fadeDuration)
                        .OnComplete(() => 
                        {
                            target.gameObject.SetActive(false);
                        });
                    break;
                case Graphic graphic:
                    graphic.DOFade(0, _fadeDuration)
                        .OnComplete(() => 
                        {
                            target.gameObject.SetActive(false);
                            
                            CheckNextBoost();
                        });
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
        
        private void CheckNextBoost()
        {
            if (_boostQueue.Count > 0)
            {
                DisplayNextBoost();
            }
            else
            {
                _isDisplayingBoost = false;
            }
        }
    }
}
