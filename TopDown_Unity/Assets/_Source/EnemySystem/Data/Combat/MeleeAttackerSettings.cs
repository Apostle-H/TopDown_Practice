using UnityEngine;

namespace EnemySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/Combat/MeleeAttackerSettings", fileName = "NewMeleeAttackerSettings")]
    public class MeleeAttackerSettings : RangeAttackerSettings
    {
        [field: SerializeField] public float TriggerRange { get; private set; }
    }
}