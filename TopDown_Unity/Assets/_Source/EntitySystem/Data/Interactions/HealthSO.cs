using UnityEngine;

namespace EntitySystem.Data.Interactions
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Health/Health", fileName = "NewHealth")]
    public class HealthSO : ScriptableObject
    {
        [field: SerializeField] public int Health { get; private set; }
    }
}