using UnityEngine;

namespace EnemySystem.Data
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/EnemyMeleeCharacteristics", fileName = "NewEnemyMeleeCharacteristics")]
    public class EnemyMeleeCharacteristicsSO : ScriptableObject
    {
        [field: SerializeField] public int Hp { get; private set; }
        [field: SerializeField] public int MoveSpeed { get; private set; }
        [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    }
}