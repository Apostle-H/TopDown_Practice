using UnityEngine;

namespace PlayerSystem.Data.Interactions
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Interactions/DraggerSettings", fileName = "NewDraggerSettings")]
    public class DraggerSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float CheckRadius { get; private set; }
        [field: SerializeField] public LayerMask DragMask { get; private set; }
    }
}