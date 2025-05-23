using Reflex.Attributes;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentMoney;
        
        private IWallet _wallet;
        private IPersistentData _persistentData;

        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();
            
            _persistentData = container.Resolve<IPersistentData>();
            _wallet = container.Resolve<IWallet>();
        }
        
        private void OnEnable()
        {
            Construct();
            
            _currentMoney.text = _wallet.GetCurrentCoins().ToString();

            _wallet.CoinsChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            _wallet.CoinsChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            _currentMoney.text = _wallet.GetCurrentCoins().ToString();
        }
    }
}
