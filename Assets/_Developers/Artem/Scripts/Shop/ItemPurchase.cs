using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    [RequireComponent(typeof(ShopItemView))]
    public class ItemPurchase : MonoBehaviour
    {
        private IWallet _wallet;
        private IPersistentData _persistentData;
        private IDataProvider _dataProvider;
        private ShopItemView _itemView;
        private ItemSelector _itemSelector;

        private void Construct()
        {
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
            _wallet = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IWallet>();
            _dataProvider = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IDataProvider>();
            _itemSelector = new ItemSelector(_persistentData);
            _itemView = GetComponent<ShopItemView>();
        }
        
        private void Awake()
        {
            Construct();
        }

        private void OnEnable()
        {
            _itemView.Clicked += OnItemViewClicked;
        }

        private void OnDisable()
        {
            _itemView.Clicked -= OnItemViewClicked;
        }
        
        private void OnItemViewClicked(ShopItemView itemView)
        {
            //переделать чтобы покупка была в попапе
            
            if(itemView.IsAvailableToBuy == false)
                return;
            
            if (itemView.Price < _wallet.GetCurrentCoins())
            {
                _wallet.Spend(itemView.Price);
                
                _itemSelector.Visit(itemView.Item);
                
                _dataProvider.SavePlayerData();
            }
        }
    }
}