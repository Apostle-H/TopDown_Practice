using UnityEngine;

namespace EnemySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/Combat/MeleeAttacker", fileName = "NewMeleeAttacker")]
    public class MeleeAttackerSO : StaticRangeAttackerSO
    {
        [field: SerializeField] public float TriggerRange { get; private set; }
    }
}