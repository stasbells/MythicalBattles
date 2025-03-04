using Reflex.Attributes;
using UnityEngine;

namespace MythicalBattles
{
    public class EquipmentBootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject _hudLayer;
        [SerializeField] private Shop _shop;

        [Inject] private IDataProvider _dataProvider;
        [Inject] private IPersistentData _persistentData;

        private void Awake()
        {
            LoadDataorInit();
            
            _hudLayer.SetActive(true);  //потом поменять на окно выбора уровня
        }

        private void LoadDataorInit()
        {
            if(_dataProvider.TryLoad() == false)
                _persistentData.PlayerData = new PlayerData(_shop.ContentItems.GetEquipmentItems());
        }
    }
}
