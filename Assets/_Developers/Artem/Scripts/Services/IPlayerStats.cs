using System;
using R3;

namespace MythicalBattles
{
    public interface IPlayerStats
    {
        public ReactiveProperty<float> MaxHealth { get; }
        public ReactiveProperty<float> Damage { get; }
        public ReactiveProperty<float> AttackSpeed { get; }
        public void UpdatePlayerData(PlayerData playerData);
        public void ResetStats();
        public void Dispose();
    }
}