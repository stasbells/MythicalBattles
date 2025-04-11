using System;
using UnityEngine;
using R3;

namespace MythicalBattles
{
    public class Health : MonoBehaviour
    {
        private const float MinHealthValue = 0;

        [SerializeField] private float _maxHealthValue;

        private Animator _animator;
        //private float _currentHealth;
        private readonly ReactiveProperty<float> _currentHealth = new();
        private readonly ReactiveProperty<float> _maxHealth = new();
        private readonly ReactiveProperty<float> _damage = new();
        private readonly ReactiveProperty<float> _heal = new();

        public float MaxHealthValue => _maxHealth.Value;
        public Observable<float> HealthValueChanged => _currentHealth;
        public Observable<float> MaxHealthValueChanged => _maxHealth;
        public Observable<float> DamageValueChanged => _damage;
        public Observable<float> HealValueChanged => _heal;

        //public event Action<float> CurrentHealthValueChanged;
        //public event Action<float> MaxHealthValueChanged;
        //public event Action<float> Damaged;
        //public event Action<float> Healed;

        public void Awake()
        {
            _animator = GetComponent<Animator>();

            _maxHealth.OnNext(_maxHealthValue);
            //_currentHealth = _maxHealthValue;
            _currentHealth.OnNext(_maxHealth.Value);
            //CurrentHealthValueChanged?.Invoke(CalculateHealthValue());
        }

        public void TakeDamage(float damage)
        {
            _damage.OnNext(damage);
            //ChangeHealthValue(_currentHealth - damage);
            ChangeHealthValue(_currentHealth.Value - _damage.Value);
            //Damaged?.Invoke(damage);
        }

        public void Heal(float health)
        {
            _heal.OnNext(health);
            //ChangeHealthValue(_currentHealth + health);
            ChangeHealthValue(_currentHealth.Value + _heal.Value);
            //Healed?.Invoke(health);
        }

        protected void ChangeMaxHealthValue(float maxHealth)
        {
            //_maxHealthValue = maxHealth;
            _maxHealth.OnNext(maxHealth);

            //MaxHealthValueChanged?.Invoke(_maxHealthValue);
            //CurrentHealthValueChanged?.Invoke(CalculateHealthValue());
        }

        private float CalculateHealthValue()
        {
            //return _currentHealth / _maxHealthValue;
            return _currentHealth.Value / _maxHealth.Value;
        }

        private void Die()
        {
            _animator.SetBool(Constants.IsDead, true);
        }

        private void ChangeHealthValue(float healthValue)
        {
            //_currentHealth = Math.Clamp(healthValue, MinHealthValue, _maxHealthValue);
            _currentHealth.OnNext(Math.Clamp(healthValue, MinHealthValue, _maxHealth.Value));

            //CurrentHealthValueChanged?.Invoke(CalculateHealthValue());

            //if (_currentHealth <= MinHealthValue)
            if (_currentHealth.Value <= MinHealthValue)
                Die();
        }
    }
}