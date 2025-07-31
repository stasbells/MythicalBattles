using DamageNumbersPro;
using DG.Tweening;
using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets.Scripts.Controllers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class HealthBar : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        [SerializeField] private DamageNumber _damageNumber;
        [SerializeField] private Image _smoothHealthBar;
        [SerializeField] private Image _healthBar;
        [SerializeField] private float _recoveryRate;
        [SerializeField] private float _healthBarScale = 0.3f;

        private RectTransform _rectTransform;
        private Coroutine _changeValueJob;
        private CanvasGroup _canvasGroup;
        private Transform _transform;
        private Health _health;
        private Camera _camera;

        private Color _healNumbersColor = new(0f, 0.81f, 0.02f);
        private Vector3 _initialLocalScale;
        private Vector3 _rotation;
        private Vector3 _scale;

        private float _initialMaxHealth;
        private float _initialScaleX;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _health = GetComponentInParent<Health>();
            _initialLocalScale = _transform.localScale;
            _scale = _transform.localScale * _healthBarScale;
            _initialScaleX = _transform.localScale.x;
            _initialMaxHealth = _health.MaxHealth.Value;
            _canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            _camera = Camera.main;
            _rotation = _camera.transform.rotation.eulerAngles;
            _canvasGroup.alpha = 0f;
        }

        private void LateUpdate()
        {
            _transform.rotation = Quaternion.Euler(_rotation);
        }

        private void OnEnable()
        {
            _health.CurrentHealthPersentValueChanged += OnCurrentHealthPercentChanged;
            _health.Damaged += OnDamaged;
            _health.Healed += OnHealed;

            _health.MaxHealth.Subscribe(OnUpdateMaxHealth).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _health.CurrentHealthPersentValueChanged -= OnCurrentHealthPercentChanged;
            _health.Damaged -= OnDamaged;
            _health.Healed -= OnHealed;

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

        private void OnDamaged(float value, Color color)
        {
            _damageNumber.Spawn(_rectTransform.position, $"-{value}", _transform, color);
        }

        private void OnHealed(float value)
        {
            _damageNumber.Spawn(_rectTransform.position, $"+{value}", _transform, _healNumbersColor);
        }

        private void AnimateBarChanging(float healthValue)
        {
            _transform.DOKill(true);

            _transform.localScale = _initialLocalScale;

            _transform.DOPunchScale(_scale, 0.5f, 15, 5f);

            _transform.localScale = _initialLocalScale;

            _canvasGroup.alpha = healthValue > 0 ? 1f : 0f;
        }
    }
}