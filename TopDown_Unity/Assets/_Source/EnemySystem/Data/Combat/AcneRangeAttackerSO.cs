using EntitySystem.Data.Combat;
using UnityEngine;

namespace EnemySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/Combat/AcneAttacker", fileName = "NewRangeAttacker")]
    public class AcneRangeAttackerSO : AttackerSO
    {
        [field: SerializeField] public float AttackRange { get; private set; }
    }
}