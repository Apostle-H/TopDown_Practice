using System;
using EntitySystem.Health;
using PlayerSystem.Data.Consumables;
using UnityEngine;

namespace PlayerSystem.Consumables
{
    public class Patch : IConsumable
    {
        private IDamageable _target;
        private PatchSO _so;

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