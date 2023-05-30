using EntitySystem.Data.Combat;
using UnityEngine;

namespace EnemySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/Combat/AcneAttacker", fileName = "NewRangeAttacker")]
    public class AcneRangeAttackerSO : AttackerSO
    {
        [field: SerializeField] public float RotationInBattle { get; private set; }
        [field: SerializeField] public int AmountResourceToRemoveShield { get; private set; }
        [field: SerializeField] public int CountAttackProjectile { get; private set; }
        [field: SerializeField] public float RotationForProjectileAxisZ { get; private set; }
    }
}