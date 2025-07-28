using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MythicalBattles.Shop
{
    [RequireComponent(typeof(Image))]
    public class ShopItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _selectedImage;
        [SerializeField] private IntValueView _priceView;

        private Image _currentBackgroundImage;
        
        public event Action<ShopItemView> Clicked;
        
        public ShopItem Item { get; private set; }
        public bool IsLock { get; private set; }
        public bool IsSelected { get; private set; }
        public bool IsAvailableToBuy { get; private set; }
        public int Price => Item.Price;

        public void Initialize(ShopItem item)
        {
            _backgroundImage.sprite = item.BackgroundImage;
            _contentImage.sprite = item.ItemImage;
            Item = item;

            _currentBackgroundImage = GetComponent<Image>();
            
            _currentBackgroundImage.sprite = _backgroundImage.sprite;
        }
        
        public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);
            
        public void Lock()
        {
            IsLock = true;
            ChangeLockVisibility(IsLock);
        }
        
        public void UnLock()
        {
            IsLock = false;
            ChangeLockVisibility(IsLock);
        }

        public void Select()
        {
            IsSelected = true;
            _selectedImage.gameObject.SetActive(true);
            HidePrice();
        }

        public void ShowPrice()
        {
            IsAvailableToBuy = true;
            _priceView.Show(Price);
        }
        
        public void HidePrice()
        {
            IsAvailableToBuy = false;
            _priceView.Hide();
        }

        private void ChangeLockVisibility(bool visibility)
        {
            _lockImage.gameObject.SetActive(visibility);
        }
    }
}
