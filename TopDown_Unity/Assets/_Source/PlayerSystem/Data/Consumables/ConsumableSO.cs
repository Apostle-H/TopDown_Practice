using UnityEngine;

namespace PlayerSystem.Data.Consumables
{
    public abstract class ConsumableSO : ScriptableObject
    {
        [field: SerializeField] public int Cost { get; private set; }
    }
}