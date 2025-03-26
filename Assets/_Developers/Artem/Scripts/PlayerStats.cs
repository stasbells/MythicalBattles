using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class PlayerStats : IPlayerStats
    {
        private const int InitMaxHealth = 100;
        private const int InitDamage = 100;
        private const int InitAttackSpeed = 100;

        public event Action<float> MaxHealthChanged;
        public event Action<float> DamageChanged;
        public event Action<float> AttackSpeedChanged;
        
        public float MaxHealth { get; private set; }
        public float Damage { get; private set; }
        public float AttackSpeed { get; private set; }

        public PlayerStats()
        {
            ResetStats();
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
        }
    }
}
