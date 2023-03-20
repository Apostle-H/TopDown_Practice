using UnityEngine;

namespace PlayerSystem
{
    public class PlayerResources
    {
        public int Count { get; private set; }

        public void Add(int amount)
        {
            Count += amount;
        }

        public bool Consume(int amount)
        {
            if (Count - amount < 0)
            {
                return false;
            }

            Count -= amount;
            return true;
        }
    }
}