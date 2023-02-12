using System;
using EntitySystem.Data.Interactions;
using EntitySystem.Health;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private HealthSO so;

        private int _currentHealth;
        private bool _isDead;
        
        public event Action OnDamaged;
        public event Action OnDeath;

        public int CurrentHealth => _currentHealth;

        private void Awake()
        {
            _currentHealth = so.Health;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
            OnDamaged?.Invoke();

            if (_currentHealth > 0 || _isDead)
            {
                return;
            }
            
            _isDead = true;
            OnDeath?.Invoke();
        }

        public void Heal(int amount)
        {
            _currentHealth = _currentHealth + amount > so.Health ? so.Health : _currentHealth + amount;
        }
    }
}