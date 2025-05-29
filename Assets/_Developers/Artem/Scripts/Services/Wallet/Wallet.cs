using System;
using UnityEngine;

namespace MythicalBattles
{
    public class Wallet : IWallet
    {
        public event Action<int> CoinsChanged;

        private IPersistentData _persistentData;
        private IDataProvider _dataProvider;

        public Wallet(IPersistentData persistentData, IDataProvider dataProvider)
        {
            _persistentData = persistentData;
            _dataProvider = dataProvider;
        }

        public void AddCoins(int coins)
        {
            _ = CheckInputIsNotNegative(coins);
            
            _persistentData.PlayerData.AddMoney(coins);
            
            CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
            
            _dataProvider.SavePlayerData();
        }

        public int GetCurrentCoins() => _persistentData.PlayerData.Money;

        public bool IsEnough(int coins)
        {
            _ = CheckInputIsNotNegative(coins);

            return _persistentData.PlayerData.Money >= coins;
        }

        public void Spend(int coins)
        {
            if (IsEnough(coins) == false)
                throw new InvalidOperationException();
                
            _persistentData.PlayerData.SpendMoney(coins);
            
            CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
        }

        private bool CheckInputIsNotNegative(int coins)
        {
            if(coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));

            return true;
        }
    }
}
