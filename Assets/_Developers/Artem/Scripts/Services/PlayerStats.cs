using System;
using R3;

namespace MythicalBattles
{
    public class PlayerStats : IPlayerStats, IDisposable
    {
        private const int InitMaxHealth = 100;
        private const int InitDamage = 50;
        private const int InitAttackSpeed = 1;

        private PlayerData _playerData;

        public ReactiveProperty<float> MaxHealth { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<float> Damage { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<float> AttackSpeed { get; } = new ReactiveProperty<float>();

        public void UpdatePlayerData(PlayerData playerData)
        {
            if (_playerData != null)
                _playerData.SelectedItemChanged -= OnInventoryItemChanged;
            
            _playerData = playerData;
            
            _playerData.SelectedItemChanged += OnInventoryItemChanged;
            
            ResetStats();
        }
        
        public void Dispose()
        {
            _playerData.SelectedItemChanged -= OnInventoryItemChanged;
        }
        
        public void IncreaseMaxHealth(float health)
        {
            MaxHealth.Value += health;
        }

        public void DecreaseMaxHealth(float health)
        {
            MaxHealth.Value -= health;
        }

        public void IncreaseDamage(float damage)
        {
            Damage.Value += damage;
        }

        public void DecreaseDamage(float damage)
        {
            Damage.Value -= damage;
        }

        public void IncreaseAttackSpeed(float attackSpeed)
        {
            AttackSpeed.Value += attackSpeed;
        }

        public void DecreaseAttackSpeed(float attackSpeed)
        {
            AttackSpeed.Value -= attackSpeed;
        }

        public void ResetStats()
        {
            MaxHealth.Value = InitMaxHealth;
            Damage.Value = InitDamage;
            AttackSpeed.Value = InitAttackSpeed;

            AcceptItemsStats();
        }

        private void OnInventoryItemChanged()
        {
            ResetStats();
        }

        private void AcceptItemsStats()
        {
            MaxHealth.Value += _playerData.GetSelectedArmor().AdditionalHealth;
            MaxHealth.Value += _playerData.GetSelectedHelmet().AdditionalHealth;
            Damage.Value += _playerData.GetSelectedWeapon().AdditionalDamage;
            Damage.Value += _playerData.GetSelectedNecklace().AdditionalDamage;
            AttackSpeed.Value += _playerData.GetSelectedBoots().AdditionalAttackSpeed;
            AttackSpeed.Value += _playerData.GetSelectedRing().AdditionalAttackSpeed;       
        }
    }
}