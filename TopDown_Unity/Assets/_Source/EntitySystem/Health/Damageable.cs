using System;
using EntitySystem.Data.Health;
using UnityEngine;

namespace EntitySystem.Health
{
    public class Damageable : MonoBehaviour
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
            _currentHealth = _currentHealth - damage >= 0 ? _currentHealth - damage : 0;
            OnDamaged?.Invoke();
            
            if (_currentHealth <= 0 && !_isDead)
            {
                _isDead = true;
                OnDeath?.Invoke();
            }
        }
    }
}