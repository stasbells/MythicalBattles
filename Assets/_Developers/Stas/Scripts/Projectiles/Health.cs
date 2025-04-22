using R3;
using System;
using UnityEngine;

namespace MythicalBattles
{
    public class Health : MonoBehaviour
    {
        private const float MinHealthValue = 0;

        [SerializeField] private float _initMaxHealthValue;
        
        private Animator _animator;
        private float _currentHealth;
    
        private readonly ReactiveProperty<float> _maxHealth = new();
        private readonly ReactiveProperty<bool> _isDead = new();

        public float MaxHealthValue => _maxHealth.Value;
        public Observable<float> MaxHealthValueChanged => _maxHealth;
        public Observable<bool>  IsDead => _isDead;

        public event Action<float> CurrentHealthPersentValueChanged;
        //public event Action<float> MaxHealthValueChanged;
        public event Action<float> Damaged;
        public event Action<float> Healed;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _maxHealth.Value = _initMaxHealthValue;
            _currentHealth = _maxHealth.Value;
            
            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());
        }
        
        private void OnEnable()
        {
            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());
        }

        public void TakeDamage(float damage)
        {
            ChangeHealthValue(_currentHealth - damage);
            Damaged?.Invoke(damage);
        }

        public void Reset()
        {
            _animator.SetBool(Constants.IsDead, false);
            _isDead.OnNext(false);
            
            _maxHealth.OnNext(_initMaxHealthValue);
            
            _currentHealth = _maxHealth.Value;

        }

        public void ApplyWaveMultiplier(float multiplier)
        {
            _maxHealth.OnNext(_initMaxHealthValue * multiplier);
            
            _currentHealth =_maxHealth.Value;
        }
        
        public void Heal(float health)
        {
            ChangeHealthValue(_currentHealth + health);
            Healed?.Invoke(health);
        }

        protected void ChangeMaxHealthValue(float maxHealth)
        {
            //_maxHealthValue = maxHealth;
            _maxHealth.OnNext(maxHealth);

            //MaxHealthValueChanged?.Invoke(_maxHealthValue);
            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());
        }

        private float CalculateHealthPercentValue()
        {
            return _currentHealth / _maxHealth.Value;
        }

        private void Die()
        {
            _animator.SetBool(Constants.IsDead, true);
            _isDead.OnNext(true);
        }
        
        private void ChangeHealthValue(float healthValue)
        {
            _currentHealth = Math.Clamp(healthValue, MinHealthValue, _maxHealth.Value);
            
            CurrentHealthPersentValueChanged?.Invoke(CalculateHealthPercentValue());
            
            if (_currentHealth <= MinHealthValue)
                Die();
        }
    }
}