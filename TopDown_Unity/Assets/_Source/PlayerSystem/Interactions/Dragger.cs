using PlayerSystem.Data.Interactions;
using UnityEngine;

namespace PlayerSystem.Interactions
{
    public class Dragger
    {
        private SpringJoint2D _enemyDragJoint;

        public bool IsDragging => _enemyDragJoint.connectedBody != null;

        public Dragger(SpringJoint2D enemyDragJoint)
        {
            _enemyDragJoint = enemyDragJoint;
        }

        public void Connect(Rigidbody2D target)
        {
            _enemyDragJoint.enabled = true;
            _enemyDragJoint.connectedBody = target;
        }

        public void Release()
        {
            _enemyDragJoint.connectedBody = default;
            _enemyDragJoint.enabled = false;
        }
    }
}