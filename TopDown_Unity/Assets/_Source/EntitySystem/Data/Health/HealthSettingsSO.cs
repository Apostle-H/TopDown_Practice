using UnityEngine;

namespace EntitySystem.Data.Health
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Health/HealthSettings", fileName = "NewHealthSettings")]
    public class HealthSettingsSO : ScriptableObject
    {
        [field: SerializeField] public int Health { get; private set; }
    }
}