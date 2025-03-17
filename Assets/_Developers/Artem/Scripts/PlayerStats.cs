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
        
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        public int AttackSpeed { get; private set; }

        public PlayerStats()
        {
            ResetStats();
        }
        
        public void IncreaseMaxHealth(int health)
        {
            MaxHealth += health;
        }

        public void DecreaseMaxHealth(int health)
        {
            MaxHealth -= health;
        }

        public void IncreaseDamage(int damage)
        {
            Damage += damage;
            Debug.Log(Damage);
        }

        public void DecreaseDamage(int damage)
        {
            Damage -= damage;
        }

        public void IncreaseAttackSpeed(int attackSpeed)
        {
            AttackSpeed += attackSpeed;
        }

        public void DecreaseAttackSpeed(int attackSpeed)
        {
            AttackSpeed -= attackSpeed;
        }

        public void ResetStats()
        {
            MaxHealth = InitMaxHealth;
            Damage = InitDamage;
            AttackSpeed = InitAttackSpeed;
        }
    }
}
