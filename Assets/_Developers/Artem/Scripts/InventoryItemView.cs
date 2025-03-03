using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MythicalBattles
{
    public class InventoryItemView : MonoBehaviour
    {
        public event Action< InventoryItemView> Clicked;
        
        [SerializeField] private Sprite _highlightBackground;

        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _backgroundImage;

        private Image _currentBackgroundImage;

        public void SetImages(Sprite content, Sprite background)
        {
            _contentImage.sprite = content;
            _backgroundImage.sprite = background;
        }
        
        public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);

        public void HighLight() => _currentBackgroundImage.sprite = _highlightBackground;
        public void UnHighLight() => _currentBackgroundImage.sprite = _backgroundImage.sprite;
    }
}
