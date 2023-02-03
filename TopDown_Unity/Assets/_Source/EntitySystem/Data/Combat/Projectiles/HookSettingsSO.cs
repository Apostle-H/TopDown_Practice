using UnityEngine;

namespace EntitySystem.Data.Combat.Projectiles
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Shooting/HookSettings", fileName = "NewHookSettings")]
    public class HookSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float ShootOutForce { get; private set; }
        [field: SerializeField] public LayerMask TargetMask { get; private set; }
    }
}