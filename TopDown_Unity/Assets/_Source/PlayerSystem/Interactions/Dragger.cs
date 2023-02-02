using PlayerSystem.Data.Interactions;
using UnityEngine;

namespace PlayerSystem.Interactions
{
    public class Dragger
    {
        private DraggerSettingsSO _settingsSO;
        private Transform _playerTransform;
        private SpringJoint2D _enemyDragJoint;

        public bool IsDragging => _enemyDragJoint.connectedBody != null;

        public Dragger(Transform playerTransform, SpringJoint2D enemyDragJoint, DraggerSettingsSO settingsSO)
        {
            _settingsSO = settingsSO;
            _playerTransform = playerTransform;
            _enemyDragJoint = enemyDragJoint;
        }

        public void Connect()
        {
            Rigidbody2D draggable = 
                Physics2D.OverlapCircle(_playerTransform.position, _settingsSO.CheckRadius, _settingsSO.DragMask)?.attachedRigidbody;
            if (draggable == null)
            {
                return;
            }
            
            _enemyDragJoint.enabled = true;
            _enemyDragJoint.connectedBody = draggable;
        }

        public void Release()
        {
            _enemyDragJoint.connectedBody = default;
            _enemyDragJoint.enabled = false;
        }
    }
}