using UnityEngine;

namespace EntitySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Shooting/HookShooter", fileName = "NewHookShooter")]
    public class HookShooterSO : ScriptableObject
    {
        [field: SerializeField] public GameObject HookPrefab { get; private set; }
    }
}