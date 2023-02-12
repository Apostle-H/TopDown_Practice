using System;
using UnityEngine;

namespace EntitySystem.Health
{
    public interface IDamageable
    {
        public event Action OnDamaged;
        public event Action OnDeath;

        public void TakeDamage(int damage);

        public void Heal(int amount);
    }
}