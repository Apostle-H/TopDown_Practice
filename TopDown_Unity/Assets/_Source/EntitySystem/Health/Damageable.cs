using System;
using EntitySystem.Data.Health;
using UnityEditor;
using UnityEngine;

namespace EntitySystem.Health
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private DamageableSettingsSO settingsSO;

        private int _currentHealth;

        public event Action OnDamaged; 
        public event Action OnDeath; 

        private void Awake()
        {
            _currentHealth = settingsSO.Health;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            OnDamaged?.Invoke();
            
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}