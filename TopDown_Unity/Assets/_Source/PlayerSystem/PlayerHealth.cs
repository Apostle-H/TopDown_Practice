using System;
using EntitySystem.Data.Interactions;
using EntitySystem.Health;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        private HealthSO _so;

        private int _currentHealth;
        private bool _isDead;
        
        public event Action OnDamaged;
        public event Action OnHeal;
        public event Action OnDeath;

        public int MaxHealth => _so.Health;
        
        public int CurrentHealth => _currentHealth;

        public void Init(HealthSO so)
        {
            _so = so;
            _currentHealth = _so.Health;
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
            _currentHealth = _currentHealth + amount > _so.Health ? _so.Health : _currentHealth + amount;
            OnHeal?.Invoke();
        }
    }
}