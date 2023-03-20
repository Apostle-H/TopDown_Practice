using UnityEngine;

namespace LevelSystem.Data
{
    [CreateAssetMenu(menuName = "SO/LevelSystem/LevelPartsSet", fileName = "NewLevelPartsSet")]
    public class LevelPartsSetSO : ScriptableObject
    {
        [field: SerializeField] public LevelPart[] LevelParts { get; private set; }
        [field: SerializeField] public LevelPart[] EmptyPart { get; private set; }
    }
}