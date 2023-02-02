using UnityEngine;

namespace EnemySystem.Data
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/EnemyRangeCharacteristics", fileName = "NewEnemyRangeCharacteristics")]
    public class EnemyRangeCharacteristicsSO : ScriptableObject
    {
        [field: SerializeField] public int Hp { get; private set; }
        [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    }
}