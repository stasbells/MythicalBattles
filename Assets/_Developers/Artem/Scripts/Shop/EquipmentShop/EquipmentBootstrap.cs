using Reflex.Attributes;
using UnityEngine;

namespace MythicalBattles
{
    public class EquipmentBootstrap : MonoBehaviour
    {
        [SerializeField] private Shop _shop;

        [Inject] private IDataProvider _dataProvider;
        [Inject] private IPersistentData _persistentData;

        private Wallet _wallet;

        private void Awake()
        {
            InitializeData();
            
            InitializeWallet();
            
            InitializeShop();
        }

        private void InitializeData()
        {
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
