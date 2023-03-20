using UnityEngine;

namespace PlayerSystem.Data.Consumables
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Consumables/Shield", fileName = "NewShied")]
    public class ShieldSO : ConsumableSO
    {
        [field: SerializeField] public float Durarion { get; private set; }
    }
}