using UnityEngine;

namespace EnemySystem.Data
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/EnemyMeleeCharacteristics", fileName = "NewEnemyMeleeCharacteristics")]
    public class EnemyMeleeSO : EnemyStaticSO
    {
        [field: SerializeField] public int MoveSpeed { get; private set; }
    }
}