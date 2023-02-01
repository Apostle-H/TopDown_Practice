using UnityEngine;

namespace PlayerSystem.Data.Movement
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Movement/MoverSettings", fileName = "NewMoverSettings")]
    public class MoverSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float Speed;
    }
}