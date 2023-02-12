using EntitySystem.Data.Interactions;
using PlayerSystem;
using UnityEngine.Rendering;

namespace UI.Player
{
    public class PlayerUIModel
    {
        private HealthSO _healthSO;

        public int MaxHealth => _healthSO.Health;

        public PlayerUIModel(HealthSO healthSO)
        {
            _healthSO = healthSO;
        }
    }
}