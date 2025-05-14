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

        private IPersistentData _persistentData;

        private void Awake()
        {
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
        }

        private void OnEnable()
        {
            ShowEquipmentItems();

            _persistentData.PlayerData.SelectedItemChanged += OnSelectedItemChange;
        }

        private void OnDisable()
        {
            _persistentData.PlayerData.SelectedItemChanged -= OnSelectedItemChange;
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
            itemView.SetImages(item.ItemImage, item.BackgroundImage);
        }
        
        private void OnSelectedItemChange()
        {
            ShowEquipmentItems();
        }
    }
}
