using MythicalBattles.Services.Data;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupEquipmentItem
{
    public class PopupEquipmentItemBinder : PopupBinder<PopupEquipmentItemViewModel>
    {
        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TMP_Text _itemStatsText;
        [SerializeField] private TMP_Text _itemTypeText;
        
        private IPersistentData _persistentData;
        private InventoryItemView _inventoryItemView;

        private void Construct()
        {
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
        }

        protected override void Start()
        {
            base.Start();

            Construct();

            _inventoryItemView = ViewModel.InventoryItemView;
            _backgroundImage.sprite = _inventoryItemView.Item.BackgroundImage;
            _contentImage.sprite = _inventoryItemView.Item.ItemImage;
            _itemStatsText.text = _inventoryItemView.Item.DisplayText;
            _itemTypeText.text = _inventoryItemView.Item.TypeText;
            _itemTypeText.color = _inventoryItemView.Item.GradeTextColor;
        }
    }
}
