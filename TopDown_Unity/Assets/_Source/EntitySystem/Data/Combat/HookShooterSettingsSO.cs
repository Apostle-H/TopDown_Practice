using UnityEngine;

namespace EntitySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Shooting/HookShooterSettings", fileName = "NewHookShooterSettings")]
    public class HookShooterSettingsSO : ScriptableObject
    {
        [field: SerializeField] public GameObject HookPrefab { get; private set; }
    }
}