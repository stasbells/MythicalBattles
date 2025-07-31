using MythicalBattles.Assets.Scripts.Services.Data;
using R3;

namespace MythicalBattles.Assets.Scripts.Services.PlayerStats
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