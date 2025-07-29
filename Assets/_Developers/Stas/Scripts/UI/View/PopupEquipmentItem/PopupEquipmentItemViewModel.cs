namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupEquipmentItem
{
    public class PopupEquipmentItemViewModel : ScreenViewModel
    {
        private readonly InventoryItemView _inventoryItemView;

        public override string Name => "PopupEquipmentItem";
        public InventoryItemView InventoryItemView => _inventoryItemView;

        public PopupEquipmentItemViewModel(InventoryItemView inventoryItemView)
        {
            _inventoryItemView = inventoryItemView;
        }
    }
}