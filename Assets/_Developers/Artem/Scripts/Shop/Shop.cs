using System;
using System.Linq;
using Reflex.Attributes;
using UnityEngine;

namespace MythicalBattles
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private EquipmentsShopContent _itemsContent;
        [SerializeField] private ShopPanel _shopPanel;
        
        private IWallet _wallet;
        private IDataProvider _dataProvider;

        [Inject]
        private void Construct(IDataProvider dataProvider, IWallet wallet)
        {
            _dataProvider = dataProvider;
            _wallet = wallet;
        }

        public EquipmentsShopContent ItemsContent => _itemsContent;

        private void Start()
        {
            _shopPanel.Show(_itemsContent.GetItems());
        }

        private void OnEnable()
        {
            _dataProvider.DataReseted += OnDataReseted;
        }

        private void OnDisable()
        {
            _dataProvider.DataReseted -= OnDataReseted;
        }

        private void OnDataReseted()
        {
            _shopPanel.Show(_itemsContent.GetItems());
        }
    }
}
