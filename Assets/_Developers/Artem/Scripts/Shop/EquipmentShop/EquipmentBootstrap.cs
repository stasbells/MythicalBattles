using UnityEngine;

namespace MythicalBattles
{
    public class ShopBootstrap : MonoBehaviour
    {
        [SerializeField] private Shop _shop;

        private IDataProvider _dataProvider;
        private IPersistentData _persistentData;

        private Wallet _wallet;

        private void Awake()
        {
            InitializeData();
            
            InitializeWallet();
            
            InitializeShop();
        }

        private void InitializeData()
        {
            _persistentData = new PersistentData();
            _dataProvider = new DataLocalProvider(_persistentData);

            LoadDataorUnit();
        }

        private void LoadDataorUnit()
        {
            if(_dataProvider.TryLoad() == false)
                _persistentData.PlayerData = new PlayerData(_shop.ContentItems.GetEquipmentItems());
        }

        private void InitializeWallet()
        {
            _wallet = new Wallet(_persistentData);
        }

        private void InitializeShop()
        {
            AllTypesSelectedItemGrade allTypesSelectedItemGrade = new AllTypesSelectedItemGrade(_persistentData);
            
            _shop.Initialize(_dataProvider, _wallet, allTypesSelectedItemGrade);
        }
        
    }
}
