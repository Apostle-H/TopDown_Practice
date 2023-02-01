using UnityEngine;

namespace PlayerSystem.Data.Shooting
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Shooting/ProjectileSettings", fileName = "NewProjectileSettings")]
    public class ProjectileSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}