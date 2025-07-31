using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using R3;
using System;
using UnityEngine;

namespace MythicalBattles
{
    public class Health : MonoBehaviour
    {
        public readonly ReactiveProperty<float> MaxHealth = new();
        public readonly ReactiveProperty<bool> IsDead = new();

        private const float MinHealthValue = 0;

        private Animator _animator;
        private float _currentHealth;
        private Color _baseDamageNumberColor = Color.white;

        public event Action<float> CurrentHealthPersentValueChanged;
        public event Action<float, Color> Damaged;
        public event Action<float> Healed;

        public Animator Animator => _animator;

        private void Awake()
        {
            OnAwakeBehaviour();

            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            OnEnableBehaviour();

            _currentHealth = MaxHealth.Value;

            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());
        }

        public virtual void TakeDamage(float damage)
        {
            ChangeHealthValue(_currentHealth - damage);
            Damaged?.Invoke(damage, _baseDamageNumberColor);
        }

        public void Heal(float healAmount)
        {
            if (healAmount <= 0)
                throw new InvalidOperationException();

            if (_currentHealth + healAmount > MaxHealth.Value)
                ChangeHealthValue(MaxHealth.Value);
            else
                ChangeHealthValue(_currentHealth + healAmount);

            Healed?.Invoke(healAmount);
        }

        protected void SetCurrentHealth() 
        {
            _currentHealth = MaxHealth.Value;
        }

        protected void TakeDamage(float damage, Color damageNumberColor)
        {
            ChangeHealthValue(_currentHealth - damage);
            Damaged?.Invoke(damage, damageNumberColor);
        }

        protected void ChangeMaxHealthValue(float maxHealth)
        {
            MaxHealth.Value = maxHealth;

            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());
        }

        protected virtual void Die()
        {
            _animator.SetBool(Constants.IsDead, true);

            IsDead.Value = true;
        }

        protected virtual void OnAwakeBehaviour() { }

        protected virtual void OnEnableBehaviour() { }

        private float CalculateHealthPercentValue()
        {
            return _currentHealth / MaxHealth.Value;
        }

        private void ChangeHealthValue(float healthValue)
        {
            _currentHealth = Math.Clamp(healthValue, MinHealthValue, MaxHealth.Value);

            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());

            if (_currentHealth <= MinHealthValue)
                Die();
        }
    }
}