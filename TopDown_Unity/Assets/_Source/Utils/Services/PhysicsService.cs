using PlayerSystem.Data.Interactions;
using UnityEngine;

namespace Utils.Services
{
    public static class PhysicsService
    {
        public static Rigidbody2D CheckArea(Transform transform, AreaCheckerSO so) =>
            Physics2D.OverlapCircle(transform.position, so.CheckRadius, so.DragMask)?.attachedRigidbody;
    }
}