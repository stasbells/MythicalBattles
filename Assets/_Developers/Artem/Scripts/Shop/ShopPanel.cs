using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class ShopPanel : MonoBehaviour
    {
        private List<ShopItemView> _shopItems = new List<ShopItemView>();

        [SerializeField] private Transform _itemsParent;
        [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

        public void Show(IEnumerable<ShopItem> items)
        {
            foreach (ShopItem item in items)
            {
                ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemsParent);

                spawnedItem.Clicked += OnItemViewClick;
                
                spawnedItem.UnSelect();
                spawnedItem.UnHighLight();
                
                //проверить открыт скин и выбран ли он
                
                _shopItems.Add(spawnedItem);
            }
        }

        private void Clear()
        {
            foreach (ShopItemView item in _shopItems)
            {
                item.Clicked -= OnItemViewClick;
                Destroy(item.gameObject);
            }
            
            _shopItems.Clear();
        }
        
        private void OnItemViewClick(ShopItemView obj)
        {
            
        }
    }
}
