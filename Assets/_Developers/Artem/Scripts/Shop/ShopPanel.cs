using System;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private ShopItemViewFactory _shopItemViewFactory;
        
        private EquipmentItemsTypes _equipmentItemsTypes;
        private AllTypesSelectedItemGrade _allTypesSelectedItemGrade;
        private List<ShopItemView> _shopItems = new List<ShopItemView>();
        public event Action<ShopItemView> ItemViewClicked;

        public void Initialize(AllTypesSelectedItemGrade allTypesSelectedItemGrade)
        {
            _equipmentItemsTypes = new EquipmentItemsTypes();
            _allTypesSelectedItemGrade = allTypesSelectedItemGrade;
        }
        
        public void Show(IEnumerable<ShopItem> items)
        {
            Clear();

            var typeIndices = new Dictionary<Type, (int selectedIndex, int currentIndex)>();
            
            foreach (ShopItem item in items)
                _equipmentItemsTypes.Visit(item);

            foreach (var type in _equipmentItemsTypes.GetTypes())
            {
                typeIndices[type] = (-1, 0);
            }

            foreach (ShopItem item in items)
            {
                ShopItemView itemView = _shopItemViewFactory.Get(item, _itemsParent);

                itemView.Clicked += OnItemViewClick;

                Type itemType = item.GetType();

                var (selectedIndex, currentIndex) = typeIndices[itemType];

                _allTypesSelectedItemGrade.Visit(item);

                EquipmentGrades selectedGrade = _allTypesSelectedItemGrade.GetGrade();

                if (selectedGrade == ((dynamic) item).EquipmentGrade)
                {
                    itemView.UnLock();
                    itemView.HidePrice();
                    itemView.Select();
                    selectedIndex = currentIndex;
                    currentIndex++;
                }
                else if (selectedIndex < 0)
                {
                    itemView.UnLock();
                    itemView.HidePrice();
                    currentIndex++;
                }
                else if (currentIndex - 1 == selectedIndex)
                {
                    itemView.UnLock();
                    itemView.ShowPrice();
                    currentIndex++;
                }
                else if (currentIndex - 1 > selectedIndex)
                {
                    itemView.HidePrice();
                    itemView.Lock();
                    currentIndex++;
                }

                typeIndices[itemType] = (selectedIndex, currentIndex);
                
                _shopItems.Add(itemView);
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

        private void OnItemViewClick(ShopItemView itemView)
        {
            Highlight(itemView);
            ItemViewClicked?.Invoke(itemView);
        }

        private void Highlight(ShopItemView shopItemView)
        {
            foreach (var itemView in _shopItems)
                itemView.UnHighLight();
            
            shopItemView.HighLight();
        }
    }
}