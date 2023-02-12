using UnityEngine;

namespace EntitySystem.Data.Interactions
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Health/HealthSettings", fileName = "NewHealthSettings")]
    public class HealthSO : ScriptableObject
    {
        [field: SerializeField] public int Health { get; private set; }
    }
}