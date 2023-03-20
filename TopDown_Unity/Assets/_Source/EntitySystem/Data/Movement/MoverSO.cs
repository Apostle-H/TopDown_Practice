using UnityEngine;

namespace EntitySystem.Data.Movement
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Movement/Mover", fileName = "NewMover")]
    public class MoverSO : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float CarrySpeed { get; private set; }
    }
}