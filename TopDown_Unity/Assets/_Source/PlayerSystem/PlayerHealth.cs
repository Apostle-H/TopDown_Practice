using System;
using EntitySystem.Data.Health;
using EntitySystem.Health;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private DamageableSettingsSO settingsSO;

        private int _currentHealth;
        private bool _isDead;
        
        public event Action OnDamaged;
        public event Action OnDeath;

        private void Awake()
        {
            _currentHealth = settingsSO.Health;
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
    }
}