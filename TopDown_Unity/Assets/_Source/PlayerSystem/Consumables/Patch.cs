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

        private int _availableAmount;

        public Patch(IDamageable target, PatchSO so)
        {
            _target = target;
            _so = so;
        }

        public void Craft()
        {
            _availableAmount++;
            
            Debug.Log(_availableAmount);
        }

        public void Use()
        {
            if (_availableAmount <= 0)
            {
                return;
            }
            
            _target.Heal(_so.HealAmount);
            _availableAmount--;
            Debug.Log(_availableAmount);
        }
    }
}