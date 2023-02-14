namespace PlayerSystem.Consumables
{
    public interface IConsumable
    {
        public int Cost { get; }

        public void Craft();
        public void Use();
    }
}