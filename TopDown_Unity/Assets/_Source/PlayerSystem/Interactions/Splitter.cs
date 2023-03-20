using EntitySystem.Interactions;

namespace PlayerSystem.Interactions
{
    public class Splitter
    {
        private readonly PlayerResources _resources;

        public Splitter(PlayerResources resources)
        {
            _resources = resources;
        }

        public void Split(ISplittable target)
        {
            _resources.Add(target.Worth);
        }
    }
}