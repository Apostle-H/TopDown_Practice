using UnityEngine;

namespace EntitySystem.Data.Shooting
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Shooting/ShooterSettings", fileName = "NewShooterSettings")]
    public class ShooterSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float ShootDelay { get; private set; }
        [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
    }
}