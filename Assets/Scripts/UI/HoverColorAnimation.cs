using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MythicalBattles
{
    [RequireComponent(typeof(Image))]
    public class HoverColorAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private Color _hoverColor = new Color(1, 0.9f, 0.5f, 1);

        private Color _originalColor;
        private Tween _сolorTween;
        private Image _targetImage;

        private void Awake()
        {
            _targetImage = GetComponent<Image>();
            _originalColor = _targetImage.color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetColorWithDuration(_hoverColor);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetColorWithDuration(_originalColor);
        }

        private void SetColorWithDuration(Color color)
        {
            _сolorTween?.Kill();
            
            _сolorTween = _targetImage.DOColor(color, _animationDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => _сolorTween = null);
        }

        private void OnDisable()
        {
            _сolorTween?.Kill();
            
            _targetImage.color = _originalColor;
        }
    }
}