using UnityEngine;

namespace EnemySystem.Data
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/EnemyStaticCharacteristics", fileName = "NewEnemyStaticCharacteristics")]
    public class EnemyStaticSO : ScriptableObject
    {
        [field: SerializeField] public LayerMask TargetMask { get; private set; }
    }
}