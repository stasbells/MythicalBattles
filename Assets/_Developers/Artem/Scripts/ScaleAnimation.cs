using DG.Tweening;
using UnityEngine;

namespace MythicalBattles
{
    public class ScaleAnimation : MonoBehaviour
    {
        [SerializeField] private float _animateTime = 0.7f;
        [SerializeField] private float _maxScale = 1.2f;

        private Tweener _tweener;

        private void OnEnable()
        {
            transform.localScale = Vector3.one;
            
            _tweener = transform.DOScale(_maxScale, _animateTime)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);;
        }

        private void OnDisable()
        {
            _tweener.Kill();
        }
    }
}
