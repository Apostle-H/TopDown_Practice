using UnityEngine;

namespace EntitySystem.Data.Movement
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Movement/Mover", fileName = "NewMover")]
    public class MoverSO : ScriptableObject
    {
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float CarrySpeed;
    }
}