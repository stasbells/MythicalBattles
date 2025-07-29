using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MythicalBattles
{
    public class HoverScaleAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private float _scaleMultiplier = 1.1f;

        private Vector3 _originalScale;
        private Tween _currentTween;
        private bool _isHovered;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovered = true;
            
            StartHoverAnimation();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
            
            _currentTween?.Kill();
            
            transform.DOScale(_originalScale, _animationDuration).SetEase(Ease.OutQuad);
        }

        private void StartHoverAnimation()
        {
            if (_currentTween != null && _currentTween.IsActive()) 
                return;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform
                .DOScale(_originalScale * _scaleMultiplier, _animationDuration).SetEase(Ease.OutQuad));

            sequence.Append(transform.DOScale(_originalScale, _animationDuration).SetEase(Ease.InQuad));

            sequence.SetLoops(-1);

            _currentTween = sequence;

            sequence.OnStepComplete(() =>
            {
                if (_isHovered) 
                    return;
                
                sequence.Kill();
                
                transform.localScale = _originalScale;
            });
        }

        private void OnDestroy()
        {
            _currentTween?.Kill();
        }
    }
}