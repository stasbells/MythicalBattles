using System.Collections.Generic;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenShop;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventoryItemView _weaponView;
        [SerializeField] private InventoryItemView _armorView;
        [SerializeField] private InventoryItemView _helmetView;
        [SerializeField] private InventoryItemView _bootsView;
        [SerializeField] private InventoryItemView _necklaceView;
        [SerializeField] private InventoryItemView _ringView;

        private List<InventoryItemView> _items; 
        private IPersistentData _persistentData;
        private IItemSelector _itemSelector;
        private ScreenShopViewModel _viewModel;

        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();
            
            _persistentData = container.Resolve<IPersistentData>();
            _itemSelector = container.Resolve<IItemSelector>();
        }

        private void Awake()
        {
            Construct();

            _items = new List<InventoryItemView>
            {
                _weaponView, _armorView, _bootsView, _helmetView, _necklaceView, _ringView
            };
        }

        private void OnEnable()
        {
            ShowEquipmentItems();

            _itemSelector.SelectedItemChanged += OnSelectedItemChange;
            
            foreach (InventoryItemView item in _items)
            {
                item.Clicked += OnItemClicked;
            }
        }

        private void OnDisable()
        {
              _itemSelector.SelectedItemChanged -= OnSelectedItemChange;
            
            foreach (InventoryItemView item in _items)
            {
                item.Clicked -= OnItemClicked;
            }
        }
        
        public void SetViewModel(ScreenShopViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void ShowEquipmentItems()
        {
            EquipmentItem weapon = _persistentData.PlayerData.GetSelectedWeapon();
            EquipmentItem armor = _persistentData.PlayerData.GetSelectedArmor();
            EquipmentItem helmet = _persistentData.PlayerData.GetSelectedHelmet();
            EquipmentItem boots = _persistentData.PlayerData.GetSelectedBoots();
            EquipmentItem necklace = _persistentData.PlayerData.GetSelectedNecklace();
            EquipmentItem ring = _persistentData.PlayerData.GetSelectedRing();
            
            ViewItem(_weaponView, weapon);
            ViewItem(_armorView, armor);
            ViewItem(_helmetView, helmet);
            ViewItem(_bootsView, boots);
            ViewItem(_necklaceView, necklace);
            ViewItem(_ringView, ring);
        }

        private void ViewItem(InventoryItemView itemView, EquipmentItem item)
        {
            itemView.Initialize(item);
        }

        private void OnItemClicked(InventoryItemView item)
        {
            _viewModel.RequestOpenPopupEquipmentItem(item);
        }
        
        private void OnSelectedItemChange()
        {
            ShowEquipmentItems();
        }
    }
}
