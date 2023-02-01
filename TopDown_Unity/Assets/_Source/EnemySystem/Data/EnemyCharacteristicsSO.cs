using UnityEngine;

namespace EnemySystem.Data
{
    [CreateAssetMenu(menuName = "SO/EnemySystem/EnemyCharacteristics", fileName = "New EnemyCharacteristics")]
    public class EnemyCharacteristicsSO : ScriptableObject
    {
        [field: SerializeField] public int Hp { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int RadiusAttack { get; private set; }
        [field: SerializeField] public int RotateSpeed { get; private set; }
        [field: SerializeField] public float DelayAttack { get; private set; } // Неизменаеммое время паузы между аттаками
        [field: SerializeField] public LayerMask Layer { get; private set; }
    }
}