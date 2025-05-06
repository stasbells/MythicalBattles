using System;
using UnityEngine;

namespace MythicalBattles
{
    public class Wallet : IWallet
    {
        public event Action<int> CoinsChanged;

        private IPersistentData _persistentData;

        public Wallet(IPersistentData persistentData) => _persistentData = persistentData;

        public void AddCoins(int coins)
        {
            _ = CheckInputIsNotNegative(coins);
            
            _persistentData.PlayerData.AddMoney(coins);
            
            CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
        }

        public int GetCurrentCoins() => _persistentData.PlayerData.Money;

        public bool IsEnough(int coins)
        {
            _ = CheckInputIsNotNegative(coins);

            return _persistentData.PlayerData.Money >= coins;
        }

        public void Spend(int coins)
        {
            _ = CheckInputIsNotNegative(coins);
            
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
