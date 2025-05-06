using System;
using UnityEngine;

namespace MythicalBattles
{
    [RequireComponent(typeof(IGetDamage))]
    public class PeriodicDamageProjectile : MonoBehaviour
    {
        [SerializeField] private float _shareOfMainDamage;
        [SerializeField] private float _damageFullTime;
        [SerializeField] private int _damageTicksCount;
        [SerializeField] private Color _damageNumbersColor;
        [SerializeField] private DamageTypes _damageType;

        private IGetDamage _mainDamageProjectile;
        private float _timeBetweenTicksDamage;
        private float _periodicDamage;
        private bool _IsFirstContactComplete;

        private void Awake()
        {
            _mainDamageProjectile = GetComponent<IGetDamage>();
            
            _timeBetweenTicksDamage = _damageFullTime / _damageTicksCount;
        }

        private void OnEnable()
        {
            _IsFirstContactComplete = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_IsFirstContactComplete)
                return;
            
            if(_mainDamageProjectile is CompanionProjectile)
                _ = TryContactWithTarget(other.gameObject);
        }

        private void OnParticleCollision(GameObject other)
        {
            if(_mainDamageProjectile is SimpleProjectile)
                _ = TryContactWithTarget(other);
        }

        private bool TryContactWithTarget(GameObject target)
        {
            if (target.layer == Constants.LayerEnemy)
            {
                _periodicDamage = (float)Math.Round(_mainDamageProjectile.GetDamage() * _shareOfMainDamage);
                
                if (target.TryGetComponent(out EnemyHealth enemyHealth) == false)
                    throw new InvalidOperationException();

                enemyHealth.TakeTickDamage(_timeBetweenTicksDamage, _periodicDamage, _damageTicksCount, _damageNumbersColor);
                
                if (target.TryGetComponent(out DamageOverTimeEffects effects) == false)
                    throw new InvalidOperationException();

                if (_damageType == DamageTypes.Fire)
                {
                    effects.PlayFireEffect(_timeBetweenTicksDamage, _damageTicksCount);
                }
                
                if (_damageType == DamageTypes.Poison)
                {
                    effects.PlayPoisonEffect(_timeBetweenTicksDamage, _damageTicksCount);
                }

                _IsFirstContactComplete = true;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
