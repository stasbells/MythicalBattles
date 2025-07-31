using System;
using MythicalBattles.Assets.Scripts.Shop.EquipmentShop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MythicalBattles.Assets.Scripts.Shop
{
    public class InventoryItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _backgroundImage;
        
        public event Action<InventoryItemView> Clicked;

        public EquipmentItem Item { get; private set; }

        public void Initialize(EquipmentItem item)
        {
            _backgroundImage.sprite = item.BackgroundImage;
            _contentImage.sprite = item.ItemImage;
            Item = item;
        }
        
        public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);
    }
}
