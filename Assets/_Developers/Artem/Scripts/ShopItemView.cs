using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MythicalBattles
{
    [RequireComponent(typeof(Image))]
    public class ShopItemView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<ShopItemView> Clicked;
        
        [SerializeField] private Sprite _highlightBackground;

        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _selectedImage;

        [SerializeField] private IntValueView _priceView;

        private Image _currentBackgroundImage;
        
        public ShopItem Item { get; private set; }
        public bool IsLock { get; private set; }
        public int Price => Item.Price;

        public GameObject Model => Item.Model;

        public void Initialize(ShopItem item)
        {
            _backgroundImage.sprite = item.BackgroundImage;
            _contentImage.sprite = item.ItemImage;
            Item = item;
            
            _priceView.Show(Price);
            
            _currentBackgroundImage = GetComponent<Image>();
            
            _currentBackgroundImage.sprite = _backgroundImage.sprite;
        }
        
        public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);

        public void Lock()
        {
            IsLock = true;
            ChangeLockVisibility(IsLock);
            _priceView.Show(Price);
        }
        
        public void UnLock()
        {
            IsLock = false;
            ChangeLockVisibility(IsLock);
            _priceView.Hide();
        }

        public void Select()
        {
            _selectedImage.gameObject.SetActive(true);
            
            //дописать применение характеристик позже
        }
        
        public void UnSelect() => _selectedImage.gameObject.SetActive(false);
        public void HighLight() => _currentBackgroundImage.sprite = _highlightBackground;
        public void UnHighLight() => _currentBackgroundImage.sprite = _backgroundImage.sprite;
        
        private void ChangeLockVisibility(bool visibility)
        {
            _lockImage.gameObject.SetActive(visibility);
        }
    }
}
