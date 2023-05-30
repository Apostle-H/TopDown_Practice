using System;
using EntitySystem.Data.Interactions;
using EntitySystem.Health;
using EntitySystem.Interactions;
using UnityEngine;
using Utils;

namespace EnemySystem.Health
{
    public class EnemyInteractions : MonoBehaviour, IEnemyDamageable, IDraggable, ISplittable
    {
        [SerializeField] private HealthSO healthSO;
        [SerializeField] private SplittableSO splittableSO;
        [SerializeField] private ParticleSystem particleKnock;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Color deadColor;
        [SerializeField] private LayerMask canKill;
        [SerializeField] private bool canGetDamage;

        private int _currentHealth;
        private bool _isKnocked;
        private bool _isDead;

        public bool CanGetDamage { get => canGetDamage; set => canGetDamage = value; }

        public event Action OnDamaged;
        public event Action OnKnock;
        public event Action OnDeath;

        public bool IsDraggable => _isKnocked && !_isDead;
        public bool IsSplittable => _isKnocked && !_isDead;
        public int Worth => splittableSO.Worth;

        private void Awake()
        {
            _currentHealth = healthSO.Health;
        }

        public void TakeDamage(int damage, LayerMask layerObject)
        {
            if (!CanGetDamage)
            {
                return;
            }
            
            _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
            OnDamaged?.Invoke();

            if (_currentHealth > 0)
            {
                return;
            }

            if (!_isKnocked
                && canKill.Contains(layerObject))
            {
                _isKnocked = true;
                sprite.color = deadColor;
                OnKnock?.Invoke();
            }
            else
            {
                particleKnock.Play();
            }
            // else if (!_isDead)
            // {
            //     _isDead = true;
            //     OnDeath?.Invoke();
            // }
        }
        
        public void Heal(int amount)
        {
            _currentHealth = _currentHealth + amount > healthSO.Health ? healthSO.Health : _currentHealth + amount;
        }
    }
}