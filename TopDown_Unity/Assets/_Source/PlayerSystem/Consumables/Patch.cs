using System;
using EntitySystem.Health;
using PlayerSystem.Data.Consumables;

namespace PlayerSystem.Consumables
{
    public class Patch : IConsumable
    {
        private readonly IDamageable _target;
        private readonly PatchSO _so;

        public int Cost => _so.Cost;
        public int Crafted { get; private set; }

        public event Action OnCrafted;
        public event Action OnUsed;

        public Patch(IDamageable target, PatchSO so)
        {
            _target = target;
            _so = so;
        }

        public void Craft()
        {
            Crafted++;
            
            OnCrafted?.Invoke();
        }

        public void Use()
        {
            if (Crafted <= 0)
            {
                return;
            }
            
            _target.Heal(_so.HealAmount);
            Crafted--;
            
            OnUsed?.Invoke();
        }
    }
}