using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class CollectionsExtensions
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            enumerable.Random(out T result);
            return result;
        }
        
        public static int Random<T>(this IEnumerable<T> enumerable, out T result)
        {
            int randomIndex = UnityEngine.Random.Range(0, enumerable.Count());
            result = enumerable.ElementAt(randomIndex);
            return randomIndex;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable)
            {
                action(element);
            }
        }
    }
}