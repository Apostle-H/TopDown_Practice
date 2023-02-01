using UnityEngine;

namespace PlayerSystem.Data.Shooting
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Shooting/HookShooterSettings", fileName = "NewHookShooterSettings")]
    public class HookShooterSettingsSO : ScriptableObject
    {
        [field: SerializeField] public GameObject HookPrefab { get; private set; }
    }
}