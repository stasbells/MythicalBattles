using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Image _healthBar;
        [SerializeField] private Slider _slider;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Transform _transform;
        private Camera _camera;
        private Vector3 _rotation;

        private void Awake()
        {
            _camera = Camera.main;
            _transform = GetComponent<Transform>();
            _slider = GetComponent<Slider>();
            _canvasGroup.alpha = 0f;
            _rotation = _camera.transform.rotation.eulerAngles;
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
            _slider.value = healthValue;

            _canvasGroup.alpha = healthValue > 0 ? 1f : 0f;
        }
    }
}