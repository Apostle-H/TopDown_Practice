using UnityEngine;

namespace EntitySystem.Data.Combat.Projectiles
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Shooting/Projectile", fileName = "NewProjectile")]
    public class ProjectileSettingsSO : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public bool StayTillLifeTime { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public LayerMask TargetMask { get; private set; }
        [field: SerializeField] public LayerMask IgnoreMask { get; private set; }
    }
}