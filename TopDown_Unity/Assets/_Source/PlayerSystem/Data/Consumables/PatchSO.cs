using UnityEngine;

namespace PlayerSystem.Data.Consumables
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Consumables/PatchSettings", fileName = "NewPatchSettings")]
    public class PatchSO : ConsumableSO
    {
        [field: SerializeField] public int HealAmount { get; private set; }
    }
}