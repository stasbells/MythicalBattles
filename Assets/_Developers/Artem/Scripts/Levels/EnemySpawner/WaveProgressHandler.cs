using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles
{
    public class WaveProgressHandler : MonoBehaviour
    {
        private readonly string _nextWaveTextFormat = "Next wave in";

        [SerializeField] private WaveProgressView _waveProgressView;
        [SerializeField] private float _smoothSpeed = 2f;
        [SerializeField] private float _fadeDuration = 0.4f;
        
        private Canvas _canvas;
        private CanvasGroup _progressSliderCanvasGroup;
        private GameObject _progressSliderObject;
        private Slider _waveProgressSlider;
        private TMP_Text _waveNumberText;
        private TMP_Text _nextWaveText;
        private TMP_Text _boostsDescriptionText;
        private BoostsDescription _boostsDescription;
        private Coroutine _smoothProgressCoroutine;
        private ParticleSystem _sliderEffect;
        private RectTransform _fillRect;
        private Tween _currentMainTween;
        private Tween _currentBoostTween;
        private int _currentWaveTotalEnemies;
        private int _defeatedEnemies;
        private int _wavesCount;
        private int _currentWaveNumber;
        private int _timeBetweenWaves;
        private int _ticksBetweenWaves;
        private Queue<Boost> _boostQueue = new Queue<Boost>();
        private bool _isDisplayingBoost;

        private IDisposable _timerSubscription;

        private void OnDestroy()
        {
            _timerSubscription?.Dispose();
            _currentMainTween?.Kill();
        }

        public void Initialize(Canvas canvas, int wavesCount, int timeBetweenWaves)
        {
            _canvas = canvas;
            _wavesCount = wavesCount;
            _timeBetweenWaves = timeBetweenWaves;
            
            WaveProgressView progressSliderView = Instantiate(_waveProgressView, _canvas.transform);
            progressSliderView.transform.SetAsFirstSibling();

            _progressSliderObject = progressSliderView.ProgressBar;
            _nextWaveText = progressSliderView.NextWaveText;
            _boostsDescription = progressSliderView.BoostsDescription;
            
            if(_boostsDescription.TryGetComponent(out TMP_Text text) == false)
                throw new InvalidOperationException();
                
            _boostsDescriptionText = text;

            if(_progressSliderObject.TryGetComponent(out CanvasGroup canvasGroup) == false)
                throw new InvalidOperationException();

            _progressSliderCanvasGroup = canvasGroup;
            
            _waveProgressSlider = _progressSliderObject.GetComponentInChildren<Slider>();
            _waveNumberText = _progressSliderObject.GetComponentInChildren<TMP_Text>();

            _progressSliderCanvasGroup.alpha = 0f;
            
            _progressSliderObject.SetActive(false);
            _nextWaveText.gameObject.SetActive(false);
            _boostsDescriptionText.gameObject.SetActive(false);
        }
        
        public void InitializeWave(int totalEnemies, int waveNumber)
        {
            if (totalEnemies <= 0)
                throw new InvalidOperationException();

            _currentWaveNumber = waveNumber;
            
            FadeIn(_progressSliderCanvasGroup, _currentMainTween);

            _waveNumberText.text = $"{waveNumber}";
            
            _currentWaveTotalEnemies = totalEnemies;
            _defeatedEnemies = 0;
        
            if(_waveProgressSlider != null)
            {
                _waveProgressSlider.value = 0f;
                if(_smoothProgressCoroutine != null)
                    StopCoroutine(_smoothProgressCoroutine);
            }
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
            
            FadeIn(_boostsDescriptionText, _currentBoostTween);
            
            _boostsDescription.Display(nextBoost);

            StartCoroutine(WaitAndFadeOut());
        }
        
        private IEnumerator WaitAndFadeOut()
        {
            yield return new WaitForSeconds(2f);
            
            FadeOut(_boostsDescriptionText, _currentBoostTween);
        }

        private void UpdateProgressSmoothly()
        {
            float targetProgress = (float)_defeatedEnemies / _currentWaveTotalEnemies;
        
            if(_smoothProgressCoroutine != null)
                StopCoroutine(_smoothProgressCoroutine);
        
            _smoothProgressCoroutine = StartCoroutine(SmoothProgressUpdate(targetProgress));
        }

        private IEnumerator SmoothProgressUpdate(float targetProgress)
        {
            float startValue = _waveProgressSlider.value;
            float elapsed = 0f;

            while(elapsed < 1f)
            {
                float currentProgress = Mathf.Lerp(startValue, targetProgress, elapsed);
                
                _waveProgressSlider.value = currentProgress;

                elapsed += Time.deltaTime * _smoothSpeed;
                
                yield return null;
            }

            _waveProgressSlider.value = targetProgress;

            if (_defeatedEnemies == _currentWaveTotalEnemies)
            {
                FadeOut(_progressSliderCanvasGroup, _currentMainTween);
                
                yield return new WaitForSeconds(_fadeDuration);

                if (_currentWaveNumber != _wavesCount)
                {
                    FadeIn(_nextWaveText, _currentMainTween);
                    
                    StartTimerForNextWave();
                }
            }
        }

        private void StartTimerForNextWave()
        {
            _timerSubscription?.Dispose();
            
            _ticksBetweenWaves = _timeBetweenWaves - 1;

            _nextWaveText.text = $"{LanguagesDictionary.GetTranslation(_nextWaveTextFormat)} {_ticksBetweenWaves}";

            _timerSubscription = Observable
                .Interval(TimeSpan.FromSeconds(1f))
                .Subscribe(_ => AddTick());
        }

        private void AddTick()
        {
            _ticksBetweenWaves--;
            _nextWaveText.text = $"{LanguagesDictionary.GetTranslation(_nextWaveTextFormat)} {_ticksBetweenWaves}";

            if (_ticksBetweenWaves == 1)
            {
                StartCoroutine(WaitForNextWaveTextFadeOut());
            }
        }

        private IEnumerator WaitForNextWaveTextFadeOut()
        {
            yield return new WaitForSeconds(_fadeDuration);
            
            FadeOut(_nextWaveText, _currentMainTween);
                
            _timerSubscription?.Dispose();
        }

        private void FadeIn(Component target, Tween tween)
        {
            target.gameObject.SetActive(true);
            
            switch (target)
            {
                case CanvasGroup canvasGroup:
                    tween = canvasGroup.DOFade(1, _fadeDuration);
                    break;
                case Graphic graphic:
                    tween = graphic.DOFade(1, _fadeDuration);
                    break;
                default:
                    Debug.LogError($"Unsupported type: {target.GetType()}");
                    break;
            }
        }
        
        private void FadeOut(Component target, Tween tween)
        {
            switch (target)
            {
                case CanvasGroup canvasGroup:
                    tween = canvasGroup.DOFade(0, _fadeDuration)
                        .OnComplete(() => 
                        {
                            target.gameObject.SetActive(false);
                        });
                    break;
                case Graphic graphic:
                    tween = graphic.DOFade(0, _fadeDuration)
                        .OnComplete(() => 
                        {
                            target.gameObject.SetActive(false);
                            
                            CheckNextBoost();
                        });
                    break;
                default:
                    Debug.LogError($"Unsupported type: {target.GetType()}");
                    break;
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
