using UnityEngine;

namespace EntitySystem.Data.Health
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Health/DamageableSettings", fileName = "NewDamageableSettings")]
    public class DamageableSettingsSO : ScriptableObject
    {
        [field: SerializeField] public int Health { get; private set; }
    }
}