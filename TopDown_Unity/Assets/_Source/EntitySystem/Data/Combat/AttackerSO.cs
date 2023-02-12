using UnityEngine;

namespace EntitySystem.Data.Combat
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Combat/Attacker", fileName = "NewAttacker")]
    public class AttackerSO : ScriptableObject
    {
        [field: SerializeField] public float ShootDelay { get; private set; }
        [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
    }
}