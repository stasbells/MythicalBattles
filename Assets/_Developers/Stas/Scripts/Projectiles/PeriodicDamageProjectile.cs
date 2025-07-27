using System;
using Ami.BroAudio;
using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using MythicalBattles.Companions;
using MythicalBattles.Services.AudioPlayback;
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

        private IGetDamage _mainDamageProjectile;
        private float _timeBetweenTicksDamage;
        private float _periodicDamage;
        private bool _IsFirstContactComplete;
        private IAudioPlayback _audioPlayback;

        private void Awake()
        {
            _mainDamageProjectile = GetComponent<IGetDamage>();
            
            _timeBetweenTicksDamage = _damageFullTime / _damageTicksCount;
            
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
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

                SoundID shotSound;

                switch (_damageType)
                {
                    case DamageTypes.Electric:

                        shotSound = _audioPlayback.AudioContainer.ElectricShot;
                        
                        _audioPlayback.PlaySound(shotSound);
                        
                        break;
                    case DamageTypes.Fire:
                        
                        effects.PlayFireEffect(_timeBetweenTicksDamage, _damageTicksCount);
                        
                        shotSound = _audioPlayback.AudioContainer.FireShot;
                        
                        _audioPlayback.PlaySound(shotSound);
                        
                        break;
                    case DamageTypes.Poison:
                        
                        effects.PlayPoisonEffect(_timeBetweenTicksDamage, _damageTicksCount);
                        
                        shotSound = _audioPlayback.AudioContainer.PoisonShot;
                        
                        _audioPlayback.PlaySound(shotSound);
                        
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
    }
}
