using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MythicalBattles
{
    public class ShopItemView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<ShopItemView> Clicked;

        [SerializeField] private Sprite _standartBackground;
        [SerializeField] private Sprite _highlightBackground;

        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _selectedImage;
        
        public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);

    }
}
