using EntitySystem.Data.Combat;
using EntitySystem.Shooting;
using UnityEngine;

namespace EnemySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/Combat/RangeAttacker", fileName = "NewRangeAttacker")]
    public class StaticRangeAttackerSO : AttackerSO
    {
        [field: SerializeField] public float AttackRange { get; private set; }
    }
}