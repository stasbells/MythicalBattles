using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DamageNumbersPro;
using R3;

namespace MythicalBattles
{
    [RequireComponent(typeof(CanvasGroup))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _smoothHealthBar;
        [SerializeField] private DamageNumber _damageNumber;
        [SerializeField] private float _recoveryRate;
        [SerializeField] private float _healthBarScale = 0.3f;

        private Health _health;
        private Camera _camera;
        private Transform _transform;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Coroutine _changeValueJob;

        private Vector3 _initialLocalScale;
        private Vector3 _rotation;
        private Vector3 _scale;
        private float _initialMaxHealth;
        private float _initialScaleX;

        private readonly CompositeDisposable _disposable = new();

        private void Awake()
        {
            _camera = Camera.main;
            _transform = GetComponent<Transform>();
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _health = GetComponentInParent<Health>();
            _initialLocalScale = _transform.localScale;
            _rotation = _camera.transform.rotation.eulerAngles;
            _scale = _transform.localScale * _healthBarScale;
            _initialScaleX = _transform.localScale.x;
            _initialMaxHealth = _health.MaxHealthValue;
            _canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            _canvasGroup.alpha = 0f;
        }

        private void LateUpdate()
        {
            _transform.rotation = Quaternion.Euler(_rotation);
        }

        private void OnEnable()
        {
            _health.CurrentHealthPersentValueChanged += OnCurrentHealthPercentChanged;
            //_health.MaxHealthValueChanged += OnUpdateMaxHealth;
            _health.Damaged += ViewHealthChange;

            //_health.HealthValueChanged.Subscribe(value => OnCurrentHealthPercentChanged(value)).AddTo(_disposable);
            _health.MaxHealthValueChanged.Subscribe(value => OnUpdateMaxHealth(value)).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _health.CurrentHealthPersentValueChanged -= OnCurrentHealthPercentChanged;
            //_health.MaxHealthValueChanged -= OnUpdateMaxHealth;
            _health.Damaged -= ViewHealthChange;

            _disposable?.Dispose();
        }

        private void OnCurrentHealthPercentChanged(float healthValue)
        {
            ChangeValue(healthValue);

            AnimateBarChanging(healthValue);
        }

        private void OnUpdateMaxHealth(float newMaxHealth)
        {
            float scaleFactor = newMaxHealth / _initialMaxHealth;
            float newScaleX = _initialScaleX * scaleFactor;

            _initialLocalScale.x = newScaleX;

            _rectTransform.localScale = new Vector3(
                newScaleX,
                _rectTransform.localScale.y,
                _rectTransform.localScale.z
            ); 

            Debug.Log(_transform.localScale);
        }

        private void ChangeValue(float healthValue)
        {
            _healthBar.fillAmount = healthValue;

            if (_changeValueJob != null)
                StopCoroutine(_changeValueJob);

            _changeValueJob = StartCoroutine(ChangeValueTo(healthValue));
        }

        private IEnumerator ChangeValueTo(float healthValue)
        {
            while (Mathf.Approximately(_smoothHealthBar.fillAmount, healthValue) == false)
            {
                _smoothHealthBar.fillAmount = Mathf.MoveTowards
                    (_smoothHealthBar.fillAmount, healthValue, _recoveryRate * Time.deltaTime);

                yield return null;
            }
        }

        private void ViewHealthChange(float value)
        {
            _damageNumber.Spawn(_rectTransform.position, value, _transform);
        }

        private void AnimateBarChanging(float healthValue)
        {
            _transform.DOPunchScale(_scale, 0.5f, 15, 5f).OnComplete(() =>
            {
                _transform.localScale = _initialLocalScale;
            });

            _canvasGroup.alpha = healthValue > 0 ? 1f : 0f;
        }
    }
}