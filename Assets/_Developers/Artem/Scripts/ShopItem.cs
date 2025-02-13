using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles
{
    public abstract class ShopItem : ScriptableObject
    {
        [SerializeField] private GameObject _model;
        [SerializeField] private Sprite _backgroundImage;
        [SerializeField] private Sprite _itemImage;
        [SerializeField, Range(0,10000)] private int _price;

        public GameObject Model => _model;
        public Sprite ItemImage => _itemImage;
        public Sprite BackgroundImage => _backgroundImage;
        public int Price => _price;

    }
}
