using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LevelSystem.Data;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Utils
{
    public static class DirectionExtensions
    {
        public static Direction GetAlternative(this Direction direction) =>
            direction switch
            {
                Direction.top => Direction.bottom,
                Direction.bottom => Direction.top,
                Direction.left => Direction.right,
                Direction.right => Direction.left,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

        public static Vector2 GetVector2(this Direction direction) =>
            direction switch
            {
                Direction.top => Vector2.up,
                Direction.bottom => Vector2.down,
                Direction.left => Vector2.left,
                Direction.right => Vector2.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        
        public static void ForEachDirection(Action<Direction> action)
        {
            foreach (Direction direction in (Direction[])Enum.GetValues(typeof(Direction)))
            {
                action(direction);
            }
        }
    }
}