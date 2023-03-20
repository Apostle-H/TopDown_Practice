using System;

namespace PlayerSystem.Consumables
{
    public interface IConsumable
    {
        public int Cost { get; }

        public event Action OnCrafted;
        public event Action OnUsed;

        public void Craft();
        public void Use();
    }
}