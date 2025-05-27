using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MythicalBattles
{
    public class InventoryItemView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<InventoryItemView> Clicked;

        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _backgroundImage;

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
