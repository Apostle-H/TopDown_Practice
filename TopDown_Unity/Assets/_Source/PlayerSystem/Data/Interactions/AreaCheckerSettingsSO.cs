using UnityEngine;

namespace PlayerSystem.Data.Interactions
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Interactions/AreaCheckerSettings", fileName = "NewAreaCheckerSettings")]
    public class AreaCheckerSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float CheckRadius { get; private set; }
        [field: SerializeField] public LayerMask DragMask { get; private set; }
    }
}