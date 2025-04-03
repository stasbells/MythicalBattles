using Reflex.Attributes;
using UnityEngine;

namespace MythicalBattles
{
    public class EquipmentBootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject _hudLayer;
        [SerializeField] private Shop _shop;

        private IDataProvider _dataProvider;
        private IPersistentData _persistentData;
        private IPlayerStats _playerStats;

        [Inject]
        private void Construct(IPersistentData persistentData, IPlayerStats playerStats, IDataProvider dataProvider)
        {
            _persistentData = persistentData;
            _dataProvider = dataProvider;
            _playerStats = playerStats;
        }

        private void Awake()
        {
            //_dataProvider.ResetData();

            _shop.ItemsContent.InitializeRegistry();
            
            LoadOrInitPlayerData();
            
            _hudLayer.SetActive(true);  //потом поменять на окно выбора уровня
        }

        private void LoadOrInitPlayerData()
        {
            if (_dataProvider.TryLoad() == false)
            {
                _persistentData.PlayerData = new PlayerData();
                
                _dataProvider.Save();
            }
            
            _persistentData.PlayerData.Initialize(_shop.ItemsContent);
            
            _playerStats.UpdatePlayerData(_persistentData.PlayerData);
        }
    }
}
