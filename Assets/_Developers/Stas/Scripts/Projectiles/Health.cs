using UnityEngine;

namespace MythicalBattles
{
    public class Health : MonoBehaviour
    {
        private readonly int _isDead = Animator.StringToHash("isDead");

        [SerializeField] private int _healthPoints;

        private Animator _animator;

        public void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(int damage)
        {
            if (_healthPoints <= damage)
                Die();
            else
                _healthPoints -= damage;
        }

        private void Die()
        {
            _healthPoints = 0;
            _animator.SetBool(_isDead, true);
        }
    }
}