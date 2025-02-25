using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles
{
    public class SmoothHealthBar : MonoBehaviour
    {
        [SerializeField] private float _recoveryRate;
        [SerializeField] private Health _health;
        [SerializeField] private Slider _slider;

        private Transform _transform;
        private Coroutine _changeValueJob;
        private Camera _camera;
        private Vector3 _rotation;

        private void Awake()
        {
            _camera = Camera.main;
            _transform = GetComponent<Transform>();
            _slider = GetComponent<Slider>();
            _rotation = _camera.transform.rotation.eulerAngles;
        }

        private void Update()
        {
            _transform.rotation = Quaternion.Euler(_rotation);
        }

        private void OnEnable()
        {
            _health.HealthValueChanged += OnChangeValue;
        }

        private void OnDisable()
        {
            _health.HealthValueChanged -= OnChangeValue;
        }

        private void OnChangeValue(float healthValue)
        {
            if (_changeValueJob != null)
                StopCoroutine(_changeValueJob);

            _changeValueJob = StartCoroutine(ChangeValueTo(healthValue));
        }

        private IEnumerator ChangeValueTo(float healthValue)
        {
            while (_slider.value != healthValue)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, healthValue, _recoveryRate * Time.deltaTime);

                yield return null;
            }
        }
    }
}