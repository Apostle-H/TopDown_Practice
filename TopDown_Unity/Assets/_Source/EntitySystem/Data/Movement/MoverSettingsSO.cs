using UnityEngine;

namespace EntitySystem.Data.Movement
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Movement/MoverSettings", fileName = "NewMoverSettings")]
    public class MoverSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float CarrySpeed;
    }
}