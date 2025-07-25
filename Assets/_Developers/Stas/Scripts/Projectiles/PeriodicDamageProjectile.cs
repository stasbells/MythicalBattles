using System;
using Ami.BroAudio;
using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private float _timeBetweenTicksDamage;
        private float _periodicDamage;
        private bool _IsFirstContactComplete;
        private IGetDamage _mainDamageProjectile;
        private IAudioPlayback _audioPlayback;

        private void Construct()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }

        private void Awake()
        {
            Construct();

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

                switch (_damageType)
                {
                    case DamageTypes.Electric:

                        OnElectricDamage();

                        break;
                    case DamageTypes.Fire:
                        
                        OnFireDamage(effects);

                        break;
                    case DamageTypes.Poison:
                        
                        OnPoisonDamage(effects);

                        break;
                    default:
                        throw new InvalidOperationException();
                }

                _IsFirstContactComplete = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnElectricDamage()
        {
            _audioPlayback.PlaySound(_audioPlayback.AudioContainer.ElectricShot);
        }

        private void OnFireDamage(DamageOverTimeEffects effects)
        {
            effects.PlayFireEffect(_timeBetweenTicksDamage, _damageTicksCount);

            _audioPlayback.PlaySound(_audioPlayback.AudioContainer.FireShot);
        }

        private void OnPoisonDamage(DamageOverTimeEffects effects)
        {
            effects.PlayPoisonEffect(_timeBetweenTicksDamage, _damageTicksCount);

            _audioPlayback.PlaySound(_audioPlayback.AudioContainer.PoisonShot);
        }
    }
}
