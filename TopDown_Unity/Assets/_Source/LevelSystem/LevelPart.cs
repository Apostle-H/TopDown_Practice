using System;
using LevelSystem.Data;
using UnityEngine;

namespace LevelSystem
{
    public class LevelPart : MonoBehaviour
    {
        [field: SerializeField] public bool TopPort { get; private set; }
        [field: SerializeField] public bool BottomPort { get; private set; }
        [field: SerializeField] public bool LeftPort { get; private set; }
        [field: SerializeField] public bool RightPort { get; private set; }
        
        [field: SerializeField] public float Size { get; private set; } = 2.5f;

        public bool this[Direction direction] =>
            direction switch
            {
                Direction.top => TopPort,
                Direction.bottom => BottomPort,
                Direction.left => LeftPort,
                Direction.right => RightPort,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
    }
}