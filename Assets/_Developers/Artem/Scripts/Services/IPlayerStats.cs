using System;

namespace MythicalBattles
{
    public interface IPlayerStats
    {
        public float MaxHealth { get; }
        public float Damage { get; }
        public float AttackSpeed { get; }
        public event Action<float> MaxHealthChanged;
        public event Action<float> DamageChanged;
        public event Action<float> AttackSpeedChanged;
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