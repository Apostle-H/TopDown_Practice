using EntitySystem.Interactions;
using UnityEngine;

namespace PlayerSystem.Interactions
{
    public class Splitter
    {
        private PlayerResources _resources;

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