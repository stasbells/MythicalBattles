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
        public void IncreaseMaxHealth(float health);
        public void DecreaseMaxHealth(float health);
        public void IncreaseDamage(float damage);
        public void DecreaseDamage(float damage);
        public void IncreaseAttackSpeed(float attackSpeed);
        public void DecreaseAttackSpeed(float attackSpeed);
        public void ResetStats();
        public void Dispose();
    }
}