using UnityEngine;

namespace PlayerSystem.Data.Interactions
{
    [CreateAssetMenu(menuName = "SO/PlayerSystem/Interactions/AreaChecker", fileName = "NewAreaChecker")]
    public class AreaCheckerSO : ScriptableObject
    {
        [field: SerializeField] public float CheckRadius { get; private set; }
        [field: SerializeField] public LayerMask DragMask { get; private set; }
    }
}