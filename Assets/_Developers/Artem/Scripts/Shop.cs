using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MythicalBattles
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private EquipmentsShopContent _contentItems;
        [SerializeField] private ShopCategoryButton _equipmentItemButton;
        [SerializeField] private ShopPanel _shopPanel;

        private void OnEnable() => _equipmentItemButton.Clicked += OnEquipmentButtonClick;
        private void OnDisable() => _equipmentItemButton.Clicked -= OnEquipmentButtonClick;
        
        private void OnEquipmentButtonClick()
        {
            _shopPanel.Show(_contentItems.EquipmentItems.Cast<ShopItem>());
        }
    }
}
