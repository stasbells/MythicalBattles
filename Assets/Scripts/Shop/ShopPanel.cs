using System;
using System.Collections.Generic;
using MythicalBattles.Assets.Scripts.Services.Data;
using MythicalBattles.Assets.Scripts.Shop.EquipmentShop;
using MythicalBattles.Assets.Scripts.UI.View.ScreenShop;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets.Scripts.Shop
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

        private IPersistentData _persistentData;
        private EquipmentItemsTypes _equipmentItemsTypes = new ();
        private AllTypesSelectedItemsGrade _allTypesSelectedItemsGrade;
        private List<ShopItemView> _shopItemViews = new ();
        private ScreenShopViewModel _viewModel;
        private IEnumerable<ShopItem> _shopItems;

        private void Awake()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _persistentData = container.Resolve<IPersistentData>();

            _allTypesSelectedItemsGrade = new AllTypesSelectedItemsGrade(_persistentData);
        }

        public void SetViewModel(ScreenShopViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Show(IEnumerable<ShopItem> items)
        {
            _shopItems = items;

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

                EquipmentGrades selectedGrade = _allTypesSelectedItemsGrade.GetGrade(item);

                if (item is EquipmentItem equipmentItem)
                {
                    if (selectedGrade == equipmentItem.EquipmentGrade)
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
                }

                typeIndices[itemType] = (selectedIndex, currentIndex);

                _shopItemViews.Add(itemView);
            }

            Canvas.ForceUpdateCanvases();
        }

        public void Refresh()
        {
            if (_shopItems == null)
                throw new InvalidOperationException();
                
            Show(_shopItems);
        }

        private void Clear()
        {
            foreach (ShopItemView item in _shopItemViews)
            {
                item.Clicked -= OnItemViewClick;
                Destroy(item.gameObject);
            }

            _shopItemViews.Clear();
        }

        private void OnItemViewClick(ShopItemView itemView)
        {
            if (itemView.IsLock)
                return;
            
            _viewModel.RequestOpenPopupShopItem(this, itemView);
        }
    }
}