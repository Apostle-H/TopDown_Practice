using EntitySystem.Health;
using PlayerSystem.Data.Consumables;
using UnityEngine;

namespace PlayerSystem.Consumables
{
    public class Patch : IConsumable
    {
        private IDamageable _target;
        private PatchSO _so;

        private int _availableAmount;

        public int Cost => _so.Cost;

        public Patch(IDamageable target, PatchSO so)
        {
            _target = target;
            _so = so;
        }

        public void Craft()
        {
            _availableAmount++;
        }

        public void Use()
        {
            if (_availableAmount <= 0)
            {
                return;
            }
            
            _target.Heal(_so.HealAmount);
            _availableAmount--;
        }
    }
}