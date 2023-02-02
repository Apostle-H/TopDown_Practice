using UnityEngine;

namespace EntitySystem.Data.Shooting
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Shooting/HookSettings", fileName = "NewHookSettings")]
    public class HookSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public LayerMask PlayerMask { get; private set; }
        [field: SerializeField] public LayerMask EnemyMask { get; private set; }
    }
}