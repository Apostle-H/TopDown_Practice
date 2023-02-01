using UnityEngine;

namespace PlayerSystem.Data.Shooting
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Shooting/HookSettings", fileName = "NewHookSettings")]
    public class HookSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}