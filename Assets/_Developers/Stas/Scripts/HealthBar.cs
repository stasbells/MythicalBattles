using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DamageNumbersPro;

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

        private Vector3 _rotation;
        private Vector3 _scale;

        private void Awake()
        {
            _camera = Camera.main;
            _transform = GetComponent<Transform>();
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _health = GetComponentInParent<Health>();
            _rotation = _camera.transform.rotation.eulerAngles;
            _scale = _transform.localScale * _healthBarScale;
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
            _health.HealthValueChanged += ChangeValue;
            _health.Damaged += ViewHealthChange;
        }

        private void OnDisable()
        {
            _health.HealthValueChanged -= ChangeValue;
            _health.Damaged -= ViewHealthChange;
        }

        private void ChangeValue(float healthValue)
        {
            OnChangeValue(healthValue);

            _transform.DOPunchScale(_scale, 0.5f, 15, 5f);
            _canvasGroup.alpha = healthValue > 0 ? 1f : 0f;
        }

        private void OnChangeValue(float healthValue)
        {
            _healthBar.fillAmount = healthValue;

            if (_changeValueJob != null)
                StopCoroutine(_changeValueJob);

            _changeValueJob = StartCoroutine(ChangeValueTo(healthValue));
        }

        private IEnumerator ChangeValueTo(float healthValue)
        {
            while (_smoothHealthBar.fillAmount != healthValue)
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
    }
}