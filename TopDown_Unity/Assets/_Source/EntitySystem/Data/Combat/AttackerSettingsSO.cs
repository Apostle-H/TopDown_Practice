using UnityEngine;

namespace EntitySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Combat/AttackerSettings", fileName = "NewAttackerSettings")]
    public class AttackerSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float ShootDelay { get; private set; }
        [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
    }
}