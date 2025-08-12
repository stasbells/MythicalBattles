using MythicalBattles.Assets.Scripts.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets.Scripts.UI.View.PopupEquipmentItem
{
    public class PopupEquipmentItemBinder : PopupBinder<PopupEquipmentItemViewModel>
    {
        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TMP_Text _itemStatsText;
        [SerializeField] private TMP_Text _itemTypeText;

        private InventoryItemView _inventoryItemView;

        protected override void OnPopupBinderStart()
        {
            _inventoryItemView = ViewModel.InventoryItemView;
            _backgroundImage.sprite = _inventoryItemView.Item.BackgroundImage;
            _contentImage.sprite = _inventoryItemView.Item.ItemImage;
            _itemStatsText.text = _inventoryItemView.Item.DisplayText;
            _itemTypeText.text = _inventoryItemView.Item.TypeText;
            _itemTypeText.color = _inventoryItemView.Item.GradeTextColor;
        }
    }
}