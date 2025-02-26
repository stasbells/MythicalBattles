using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace MythicalBattles
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _smoothHealthBar;
        [SerializeField] private Health _health;
        [SerializeField] private float _recoveryRate;


        private TMP_Text _healthText;
        private Camera _camera;
        private Transform _transform;
        private CanvasGroup _canvasGroup;
        private Coroutine _changeValueJob;
        private Vector3 _rotation;
        private Vector3 _scale;

        private void Awake()
        {
            _camera = Camera.main;
            _healthText = GetComponentInChildren<TMP_Text>();
            _transform = GetComponent<Transform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _rotation = _camera.transform.rotation.eulerAngles;
            _scale = _transform.localScale * 0.3f;
            _canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            _canvasGroup.alpha = 0f;
        }

        private void Update()
        {
            _transform.rotation = Quaternion.Euler(_rotation);
        }

        private void OnEnable()
        {
            _health.HealthValueChanged += ChangeValue;
        }

        private void OnDisable()
        {
            _health.HealthValueChanged -= ChangeValue;
        }

        private void ChangeValue(float healthValue)
        {
            OnChangeValue(healthValue);

            ViewDamage(_health.MaxHealthValue - healthValue);

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
                _smoothHealthBar.fillAmount = Mathf.MoveTowards(_smoothHealthBar.fillAmount, healthValue, _recoveryRate * Time.deltaTime);

                yield return null;
            }
        }

        private void ViewDamage(float damage)
        {
            _healthText.text = damage.ToString();
            _healthText.color = Color.white;
            _healthText.DOFade(0, 1f);
        }   
    }
}