using PlayerSystem.Data.Interactions;
using UnityEngine;

namespace PlayerSystem.Interactions
{
    public class AreaChecker
    {
        private AreaCheckerSettingsSO _settingsSO;
        private Transform _transform;

        public AreaChecker(Transform transform, AreaCheckerSettingsSO settingsSO)
        {
            _settingsSO = settingsSO;
            _transform = transform;
        }

        public Rigidbody2D CheckArea() =>
            Physics2D.OverlapCircle(_transform.position, _settingsSO.CheckRadius, _settingsSO.DragMask)?.attachedRigidbody;
    }
}