using System;

namespace EntitySystem.Health
{
    public interface IEnemyDamageable : IDamageable
    {
        public event Action OnKnock;
    }
}