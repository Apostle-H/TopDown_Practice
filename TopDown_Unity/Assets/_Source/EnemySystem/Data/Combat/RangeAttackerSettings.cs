using EntitySystem.Data.Combat;
using EntitySystem.Shooting;
using UnityEngine;

namespace EnemySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/Combat/RangeAttackerSettings", fileName = "NewRangeAttackerSettings")]
    public class RangeAttackerSettings : AttackerSettingsSO
    {
        [field: SerializeField] public float AttackRange { get; private set; }
    }
}