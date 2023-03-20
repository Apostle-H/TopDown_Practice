using UnityEngine;

namespace PlayerSystem.Data.Consumables
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Consumables/Patch", fileName = "NewPatch")]
    public class PatchSO : ConsumableSO
    {
        [field: SerializeField] public int HealAmount { get; private set; }
    }
}