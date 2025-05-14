using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupB
{
    public class PopupShopItemBinder : PopupBinder<PopupShopItemViewModel>
    {
        [SerializeField] private TMP_Text _itemNameText;
        [SerializeField] private TMP_Text _itemStatsText;

        [SerializeField] private Button _buyItemButton;

        public System.Func<object, object> ItemViewClicked { get; internal set; }

        private void OnEnable()
        {
            //_buyItemButton.onClick.AddListener(OnBuyItemButtonClicked);
        }

        private void OnDisable()
        {
            //_buyItemButton.onClick.RemoveListener(OnBuyItemButtonClicked);
        }

        private void OnBuyItemButtonClicked()
        {
            
        }
    }
}