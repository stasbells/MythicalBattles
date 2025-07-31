using MythicalBattles.Assets.Scripts.Services.Data;
using MythicalBattles.Assets.Scripts.Services.Wallet;
using MythicalBattles.Assets.Scripts.Shop.EquipmentShop;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets.Scripts.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private EquipmentsShopContent _itemsContent;
        [SerializeField] private ShopPanel _shopPanel;
        
        private IWallet _wallet;
        private IDataProvider _dataProvider;

        public ShopPanel ShopPanel => _shopPanel;

        private void Awake()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _dataProvider = container.Resolve<IDataProvider>();
            _wallet = container.Resolve<IWallet>();
        }

        public EquipmentsShopContent ItemsContent => _itemsContent;

        private void Start()
        {
            ShowItems();
        }

        private void OnEnable()
        {
            _dataProvider.PlayerDataReseted += OnDataReseted;
        }

        private void OnDisable()
        {
            _dataProvider.PlayerDataReseted -= OnDataReseted;
        }

        private void ShowItems()
        {
            _shopPanel.Show(_itemsContent.GetItems());
        }

        private void OnDataReseted()
        {
            ShowItems();
        }
    }
}
