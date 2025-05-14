using R3;
using System;
using UnityEngine;

namespace MythicalBattles
{
    public class Health : MonoBehaviour
    {
        private const float MinHealthValue = 0;
        
        protected Animator Animator;
        protected float CurrentHealth;
        private Color _baseDamageNumberColor = Color.white;
    
        public readonly ReactiveProperty<float> MaxHealth = new();
        public readonly ReactiveProperty<bool> IsDead = new();
        
        public event Action<float> CurrentHealthPersentValueChanged;
        public event Action<float, Color> Damaged;
        public event Action<float> Healed;

        private void Awake()
        {
            OnAwakeBehaviour();
            
            Animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            OnEnableBehaviour();
            
            CurrentHealth = MaxHealth.Value;
            
            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());
        }

        public void TakeDamage(float damage)
        {
            ChangeHealthValue(CurrentHealth - damage);
            Damaged?.Invoke(damage, _baseDamageNumberColor);
        }

        public void TakeDamage(float damage, Color damageNumberColor)
        {
            ChangeHealthValue(CurrentHealth - damage);
            Damaged?.Invoke(damage, damageNumberColor);
        }

        public void Heal(float healAmount)
        {
            if(healAmount <= 0)
                throw new InvalidOperationException();
            
            if(CurrentHealth + healAmount > MaxHealth.Value)
                ChangeHealthValue(MaxHealth.Value);
            else
                ChangeHealthValue(CurrentHealth + healAmount);
            
            Healed?.Invoke(healAmount);
        }
        
        protected virtual void OnAwakeBehaviour()
        {
        }
        
        protected virtual void OnEnableBehaviour()
        {
        }

        protected void ChangeMaxHealthValue(float maxHealth)
        {
            MaxHealth.Value = maxHealth;
            
            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());
        }

        private float CalculateHealthPercentValue()
        {
            return CurrentHealth / MaxHealth.Value;
        }

        private void ChangeHealthValue(float healthValue)
        {
            CurrentHealth = Math.Clamp(healthValue, MinHealthValue, MaxHealth.Value);
            
            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());
            
            if (CurrentHealth <= MinHealthValue)
                Die();
        }
        
        private void Die()
        {
            Animator.SetBool(Constants.IsDead, true);
            IsDead.Value = true;
        }
    }
}