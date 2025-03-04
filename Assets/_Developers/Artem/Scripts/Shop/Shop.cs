using System.Linq;
using Reflex.Attributes;
using UnityEngine;

namespace MythicalBattles
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private EquipmentsShopContent _contentItems;
        [SerializeField] private ShopPanel _shopPanel;
        
        [Inject] private IWallet _wallet;

        public EquipmentsShopContent ContentItems => _contentItems;

        private void Start()
        {
            _shopPanel.Show(_contentItems.GetEquipmentItems().Cast<ShopItem>());
        }
    }
}
