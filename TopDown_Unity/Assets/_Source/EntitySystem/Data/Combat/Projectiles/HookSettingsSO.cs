using UnityEngine;

namespace EntitySystem.Data.Combat.Projectiles
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Shooting/Hook", fileName = "NewHook")]
    public class HookSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float ShootOutForce { get; private set; }
        [field: SerializeField] public LayerMask TargetMask { get; private set; }
    }
}