using Reflex.Attributes;
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
        
        [Inject] private IPersistentData _persistentData;
        
        private void OnEnable()
        {
            ShowEquipmentItems();
        }

        private void ShowEquipmentItems()
        {
            EquipmentItem weapon = _persistentData.PlayerData.SelectedWeapon;
            EquipmentItem armor = _persistentData.PlayerData.SelectedArmor;
            EquipmentItem helmet = _persistentData.PlayerData.SelectedHelmet;
            EquipmentItem boots = _persistentData.PlayerData.SelectedBoots;
            EquipmentItem necklace = _persistentData.PlayerData.SelectedNecklace;
            EquipmentItem ring = _persistentData.PlayerData.SelectedRing;
            
            ViewItem(_weaponView, weapon);
            ViewItem(_armorView, armor);
            ViewItem(_helmetView, helmet);
            ViewItem(_bootsView, boots);
            ViewItem(_necklaceView, necklace);
            ViewItem(_ringView, ring);
        }

        private void ViewItem(InventoryItemView itemView, EquipmentItem item)
        {
            itemView.SetImages(item.ItemImage, item.BackgroundImage);
        }
        
        
    }
}
