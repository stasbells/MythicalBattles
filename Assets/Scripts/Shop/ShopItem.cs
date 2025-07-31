using MythicalBattles.Shop.EquipmentShop;
using UnityEngine;

namespace MythicalBattles.Shop
{
    public abstract class ShopItem : ScriptableObject, IVisitorAcceptor
    {
        [SerializeField] private GameObject _model;
        [SerializeField] private Sprite _backgroundImage;
        [SerializeField] private Sprite _itemImage;
        [SerializeField, Range(0,10000)] private int _price;
        
        public string ItemID => this.name;
        public Sprite ItemImage => _itemImage;
        public Sprite BackgroundImage => _backgroundImage;
        public int Price => _price;
        public abstract string DisplayText { get; }
        public abstract void Accept(IShopItemVisitor visitor);
    }
}
