using UnityEngine;

namespace EntitySystem.Data.Interactions
{
    [CreateAssetMenu(menuName = "SO/EntitySystem/Interactions/Splittable", fileName = "NewSplittable")]
    public class SplittableSO : ScriptableObject
    {
        [field: SerializeField] public int Worth { get; private set; }
    }
}