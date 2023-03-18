using System;
using LevelSystem.Data;
using UnityEngine;

namespace LevelSystem
{
    public class LevelPart : MonoBehaviour
    {
        [field: SerializeField] public bool TopSocket { get; private set; }
        [field: SerializeField] public bool BottomSocket { get; private set; }
        [field: SerializeField] public bool LeftSocket { get; private set; }
        [field: SerializeField] public bool RightSocket { get; private set; }
        
        [field: SerializeField] public float Size { get; private set; } = 2.5f;

        public bool this[Direction direction] =>
            direction switch
            {
                Direction.top => TopSocket,
                Direction.bottom => BottomSocket,
                Direction.left => LeftSocket,
                Direction.right => RightSocket,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
    }
}