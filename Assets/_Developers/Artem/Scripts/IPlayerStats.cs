using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public interface IPlayerStats
    {
        public void IncreaseMaxHealth(int health);
        public void DecreaseMaxHealth(int health);
        public void IncreaseDamage(int damage);
        public void DecreaseDamage(int damage);
        public void IncreaseAttackSpeed(int attackSpeed);
        public void DecreaseAttackSpeed(int attackSpeed);
        public void ResetStats();
    }
}
