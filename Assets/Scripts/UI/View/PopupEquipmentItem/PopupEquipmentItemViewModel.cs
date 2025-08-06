using MythicalBattles.Assets.Scripts.Shop;

namespace MythicalBattles.Assets.Scripts.UI.View.PopupEquipmentItem
{
    public class PopupEquipmentItemViewModel : ScreenViewModel
    {
        public PopupEquipmentItemViewModel(InventoryItemView inventoryItemView)
        {
            InventoryItemView = inventoryItemView;
        }
        
        public override string Name => "PopupEquipmentItem";
        public InventoryItemView InventoryItemView { get; }
    }
}