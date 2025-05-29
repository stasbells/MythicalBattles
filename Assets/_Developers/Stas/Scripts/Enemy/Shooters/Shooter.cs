using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using System.Collections;
using UnityEngine;

namespace MythicalBattles
{
    public abstract class Shooter : MonoBehaviour
    {
        [SerializeField] private float _initRateOfFire = 1f;
        [SerializeField] private float _shootDelay = 0.3f;

        protected Animator _animator;
        private Coroutine _shootCoroutine;
        private float _shootSpeedAnimationMultiplier;
        private float _rateOfFire;
        private float _restTimer;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(Constants.IsAttack, false);

            _rateOfFire = _initRateOfFire;

            if (this is PlayerShooter)
                ApplyShootAnimationSpeed(_rateOfFire);

            OnAwake();
        }

        private void OnEnable()
        {
            _restTimer = 0f;
        }

        private void Update()
        {
            if (_animator.GetBool(Constants.IsDead))
                return;

            if (!_animator.GetBool(Constants.IsMove) && _animator.GetBool(Constants.IsAttack))
            {
                _restTimer += Time.deltaTime;

                if (_restTimer >= _shootDelay)
                    StartShootCoroutine();
            }
            else if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
                _shootCoroutine = null;
                _restTimer = 0f;
            }
        }

        protected void ChangeAttackSpeed(float attackSpeedFactor)
        {
            _rateOfFire = _initRateOfFire / attackSpeedFactor;

            ApplyShootAnimationSpeed(_rateOfFire);
        }

        protected virtual void Shoot() { }

        protected virtual void OnAwake() { }


        private void ApplyShootAnimationSpeed(float rateOfFire)
        {
            _shootSpeedAnimationMultiplier = 1 / rateOfFire; ;

            _animator.SetFloat("shootSpeed", _shootSpeedAnimationMultiplier);
        }

        private void StartShootCoroutine()
        {
            _shootCoroutine ??= StartCoroutine(ShootWithFrequency());
        }

        private IEnumerator ShootWithFrequency()
        {
            while (!_animator.GetBool(Constants.IsDead))
            {
                Shoot();

                yield return new WaitForSeconds(_rateOfFire);
            }
        }
    }
}