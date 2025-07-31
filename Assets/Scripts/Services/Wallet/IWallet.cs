using System;

namespace MythicalBattles.Assets.Scripts.Services.Wallet
{
    public interface IWallet
    {
        public event Action<int> CoinsChanged;
        public void AddCoins(int coins);
        public int GetCurrentCoins();
        public void Spend(int coins);
    }
}
