using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private EquipmentsShopContent _itemsContent;
        [SerializeField] private ShopPanel _shopPanel;
        
        private IWallet _wallet;
        private IDataProvider _dataProvider;

        public ShopPanel ShopPanel => _shopPanel;

        //[Inject]
        //public void Construct(IDataProvider dataProvider, IWallet wallet, Container container)
        //{
        //    _dataProvider = dataProvider;
        //    _wallet = wallet;
        //}

        private void Awake()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _dataProvider = container.Resolve<IDataProvider>();
            _wallet = container.Resolve<IWallet>();
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
