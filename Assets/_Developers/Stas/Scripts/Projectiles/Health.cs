using System;
using UnityEngine;
using UnityEngine.Events;

namespace MythicalBattles
{
    public class Health : MonoBehaviour
    {
        private const float MinHealthValue = 0;
        private readonly int _isDead = Animator.StringToHash("isDead");

        [SerializeField] private float _maxHealthValue;

        private Animator _animator;
        private float _currentHealth;

        public float MaxHealthValue => _maxHealthValue;

        public event UnityAction<float> HealthValueChanged;
        public event UnityAction<float> Damaged;
        public event UnityAction<float> Healed;

        public void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealthValue;
            HealthValueChanged?.Invoke(CalculateHealthValue());
        }
   
        public void TakeDamage(float damage)
        {
            ChangeHealthValue(_currentHealth - damage);
            Damaged?.Invoke(damage);
        }

        public void Heal(float health)
        {
            ChangeHealthValue(_currentHealth + health);
            Healed?.Invoke(health);
        }

        private void Die()
        {
            _animator.SetBool(_isDead, true);
        }

        private void ChangeHealthValue(float healthValue)
        {
            _currentHealth = Math.Clamp(healthValue, MinHealthValue, _maxHealthValue);

            HealthValueChanged?.Invoke(CalculateHealthValue());

            if (_currentHealth <= MinHealthValue)
                Die();
        }

        private float CalculateHealthValue()
        {
            return _currentHealth / _maxHealthValue;
        }
    }
}