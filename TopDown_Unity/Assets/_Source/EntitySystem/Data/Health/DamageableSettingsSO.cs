using UnityEngine;

namespace EntitySystem.Data.Health
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Health/DamageableSettings", fileName = "NewDamageableSettings")]
    public class DamageableSettingsSO : ScriptableObject
    {
        public int Health { get; private set; }
    }
}