using System.Linq;
using UnityEngine;

namespace MythicalBattles
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private EquipmentsShopContent _contentItems;
        [SerializeField] private ShopTestButton _equipmentItemButton;
        [SerializeField] private ShopPanel _shopPanel;

        private IDataProvider _dataProvider;
        private ShopItemView _previewedItem;
        private Wallet _wallet;
        private AllTypesSelectedItemGrade _allTypesSelectedItemGrade;

        public EquipmentsShopContent ContentItems => _contentItems;
        
        private void OnEnable() => _equipmentItemButton.Clicked += OnEquipmentButtonClick;
        private void OnDisable() => _equipmentItemButton.Clicked -= OnEquipmentButtonClick;

        public void Initialize(IDataProvider dataProvider, Wallet wallet,
            AllTypesSelectedItemGrade allTypesSelectedItemGrade)
        {
            _wallet = wallet;
            _dataProvider = dataProvider;
            _allTypesSelectedItemGrade = allTypesSelectedItemGrade;

            _shopPanel.Initialize(allTypesSelectedItemGrade);
        }
        
        private void OnEquipmentButtonClick()
        {
            _shopPanel.Show(_contentItems.GetEquipmentItems().Cast<ShopItem>());
        }
    }
}
