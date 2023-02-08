using EntitySystem.Data.Health;
using PlayerSystem;
using UnityEngine.Rendering;

namespace UI.Player
{
    public class playerUIModel
    {
        private HealthSettingsSO _healthSettingsSO;

        public int MaxHealth => _healthSettingsSO.Health;

        public playerUIModel(HealthSettingsSO healthSettingsSO)
        {
            _healthSettingsSO = healthSettingsSO;
        }
    }
}