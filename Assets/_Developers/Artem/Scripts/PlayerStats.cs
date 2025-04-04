using System;
using UnityEngine;

namespace MythicalBattles
{
    public class PlayerStats : IPlayerStats, IDisposable
    {
        private const int InitMaxHealth = 100;
        private const int InitDamage = 10;
        private const int InitAttackSpeed = 1;

        private PlayerData _playerData;
        
        public float MaxHealth { get; private set; }
        public float Damage { get; private set; }
        public float AttackSpeed { get; private set; }
        
        public event Action<float> MaxHealthChanged;
        public event Action<float> DamageChanged;
        public event Action<float> AttackSpeedChanged;

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
            MaxHealth += health;
            MaxHealthChanged?.Invoke(MaxHealth);
        }

        public void DecreaseMaxHealth(float health)
        {
            MaxHealth -= health;
            MaxHealthChanged?.Invoke(MaxHealth);
        }

        public void IncreaseDamage(float damage)
        {
            Damage += damage;
            DamageChanged?.Invoke(Damage);
        }

        public void DecreaseDamage(float damage)
        {
            Damage -= damage;
            DamageChanged?.Invoke(Damage);
        }

        public void IncreaseAttackSpeed(float attackSpeed)
        {
            AttackSpeed += attackSpeed;
            AttackSpeedChanged?.Invoke(AttackSpeed);
        }

        public void DecreaseAttackSpeed(float attackSpeed)
        {
            AttackSpeed -= attackSpeed;
            AttackSpeedChanged?.Invoke(AttackSpeed);
        }

        public void ResetStats()
        {
            MaxHealth = InitMaxHealth;
            Damage = InitDamage;
            AttackSpeed = InitAttackSpeed;

            AcceptItemsStats();
        }

        private void OnInventoryItemChanged()
        {
            ResetStats();
        }

        private void AcceptItemsStats()
        {
            MaxHealth += _playerData.GetSelectedArmor().AdditionalHealth;
            MaxHealth += _playerData.GetSelectedHelmet().AdditionalHealth;
            Damage += _playerData.GetSelectedWeapon().AdditionalDamage;
            Damage += _playerData.GetSelectedNecklace().AdditionalDamage;
            AttackSpeed += _playerData.GetSelectedBoots().AdditionalAttackSpeed;
            AttackSpeed += _playerData.GetSelectedRing().AdditionalAttackSpeed;       
        }
    }
}