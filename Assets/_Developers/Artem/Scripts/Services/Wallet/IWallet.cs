using System;

namespace MythicalBattles
{
    public interface IWallet
    {
        public event Action<int> CoinsChanged;
        public void AddCoins(int coins);
        public int GetCurrentCoins();
        public bool IsEnough(int coins);
        public void Spend(int coins);
    }
}
