using System;
using System.Collections;
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
        private Tween _currentTween;
        private int _currentWaveTotalEnemies;
        private int _defeatedEnemies;
        private int _wavesCount;
        private int _currentWaveNumber;
        private int _timeBetweenWaves;
        private int _ticksBetweenWaves;
        
        private IDisposable _timerSubscription;

        private void OnDestroy()
        {
            _timerSubscription?.Dispose();
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
            
            FadeIn(_progressSliderCanvasGroup);

            _waveNumberText.text = $"{waveNumber}";
            
            _currentWaveTotalEnemies = totalEnemies;
            _defeatedEnemies = 0;
        
            if(_waveProgressSlider != null)
            {
                _waveProgressSlider.value = 0f;
                if(_smoothProgressCoroutine != null)
                    StopCoroutine(_smoothProgressCoroutine);
            }
            
            //UpdateEffectPosition(0f);
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
            FadeIn(_boostsDescriptionText);
            
            _boostsDescription.Display(boost);

            boost.Applied -= OnBoostApplied;

            StartCoroutine(WaitForBoostDescriptionFadeOut());
        }

        private void UpdateProgressSmoothly()
        {
            float targetProgress = (float)_defeatedEnemies / _currentWaveTotalEnemies;
        
            if(_smoothProgressCoroutine != null)
                StopCoroutine(_smoothProgressCoroutine);
        
            _smoothProgressCoroutine = StartCoroutine(SmoothProgressUpdate(targetProgress));
        }
        
        private void UpdateEffectPosition(float progress)
        {
            if(_fillRect == null || _sliderEffect == null) 
                throw new InvalidOperationException();

            RectTransform effectTransform = _sliderEffect.GetComponent<RectTransform>();
            
            effectTransform.anchoredPosition = CalculatePosition(_fillRect.rect, progress);
        }
        
        private Vector2 CalculatePosition(Rect fillRect, float progress)
        {
            return new Vector2(fillRect.width * progress - fillRect.width/2f, 0);
        }

        private IEnumerator SmoothProgressUpdate(float targetProgress)
        {
            float startValue = _waveProgressSlider.value;
            float elapsed = 0f;

            while(elapsed < 1f)
            {
                float currentProgress = Mathf.Lerp(startValue, targetProgress, elapsed);
                
                _waveProgressSlider.value = currentProgress;
                
                //UpdateEffectPosition(currentProgress);
                
                elapsed += Time.deltaTime * _smoothSpeed;
                
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
            
            FadeOut(_nextWaveText);
                
            _timerSubscription?.Dispose();
        }

        private IEnumerator WaitForBoostDescriptionFadeOut()
        {
            yield return new WaitForSeconds(2);
            
            FadeOut(_boostsDescriptionText);
        }

        private void FadeIn(Component target)
        {
            _currentTween?.Kill();
            
            target.gameObject.SetActive(true);
            
            switch (target)
            {
                case CanvasGroup canvasGroup:
                    _currentTween = canvasGroup.DOFade(1, _fadeDuration);
                    break;
                case Graphic graphic: 
                    _currentTween = graphic.DOFade(1, _fadeDuration);
                    break;
                default:
                    Debug.LogError($"Unsupported type: {target.GetType()}");
                    break;
            }
        }
        
        private void FadeOut(Component target)
        {
            _currentTween?.Kill();
            
            switch (target)
            {
                case CanvasGroup canvasGroup:
                    
                    _currentTween = canvasGroup.DOFade(0, _fadeDuration)
                        .OnComplete(() => 
                        {
                            target.gameObject.SetActive(false);
                        });
                    
                    break;
                case Graphic graphic: 
                   
                    _currentTween = graphic.DOFade(0, _fadeDuration)
                        .OnComplete(() => 
                        {
                            target.gameObject.SetActive(false);
                        });
                    
                    break;
                default:
                    Debug.LogError($"Unsupported type: {target.GetType()}");
                    break;
            }
        }
    }
}
