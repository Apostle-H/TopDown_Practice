using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class MyExtensions
    {
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        public static Quaternion LookAt2D(this Transform transform, Vector3 target)
        {
            Vector3 diff = target - transform.position;
            diff.Normalize();
 
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }
}