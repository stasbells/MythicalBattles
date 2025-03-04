using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private Color hoverColor = new Color(1, 0.9f, 0.5f, 1);

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
            StartHoverEffect();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EndHoverEffect();
        }

        private void StartHoverEffect()
        {
            _сolorTween?.Kill();
            
            _сolorTween = _targetImage.DOColor(hoverColor, _animationDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => _сolorTween = null);
        }

        private void EndHoverEffect()
        {
            _сolorTween?.Kill();
            
            _сolorTween = _targetImage.DOColor(_originalColor, _animationDuration)
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