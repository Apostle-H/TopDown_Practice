using System;

namespace PlayerSystem
{
    public class PlayerResources
    {
        public int Amount { get; private set; }

        public event Action OnAdded;
        public event Action OnConsumed;

        public void Add(int amount)
        {
            Amount += amount;
            
            OnAdded?.Invoke();
        }

        public bool Consume(int amount)
        {
            if (Amount - amount < 0)
            {
                return false;
            }

            Amount -= amount;
            
            OnConsumed?.Invoke();
            return true;
        }
    }
}