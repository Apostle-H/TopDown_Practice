using UnityEngine;

namespace EntitySystem.Data.Combat.Projectiles
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Shooting/HookSettings", fileName = "NewHookSettings")]
    public class HookSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public LayerMask PlayerMask { get; private set; }
        [field: SerializeField] public LayerMask EnemyMask { get; private set; }
    }
}