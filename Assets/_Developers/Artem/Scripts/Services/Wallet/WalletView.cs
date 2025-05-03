using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace MythicalBattles
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentMoney;
        
        [Inject] private IWallet _wallet;
        [Inject] private IPersistentData _persistentData;

        private void OnEnable()
        {
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
