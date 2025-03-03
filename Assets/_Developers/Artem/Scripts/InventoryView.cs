using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Awake()
        {
            
        }
    }
}
