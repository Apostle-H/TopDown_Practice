using System;
using EntitySystem.Data.Health;
using EntitySystem.Health;
using EntitySystem.Interactions;
using UnityEngine;

namespace EnemySystem.Health
{
    public class EnemyInteractions : MonoBehaviour, IEnemyDamageable, IDraggable
    {
        [SerializeField] private HealthSettingsSO settingsSO;

        private int _currentHealth;
        private bool _isKnocked;
        private bool _isDead;

        public event Action OnDamaged;
        public event Action OnKnock;
        public event Action OnDeath;

        public bool IsDraggable => _isKnocked && !_isDead;

        private void Awake()
        {
            _currentHealth = settingsSO.Health;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
            OnDamaged?.Invoke();

            if (_currentHealth > 0)
            {
                return;
            }

            if (!_isKnocked)
            {
                _isKnocked = true;
                OnKnock?.Invoke();
            }
            else if (!_isDead)
            {
                _isDead = true;
                OnDeath?.Invoke();
            }
        }
    }
}