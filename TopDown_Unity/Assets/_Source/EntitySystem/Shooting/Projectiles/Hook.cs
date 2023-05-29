using System;
using EntitySystem.Data.Combat.Projectiles;
using EntitySystem.Health;
using EntitySystem.Interactions;
using UnityEngine;
using Utils;

namespace EntitySystem.Shooting.Projectiles
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private HookSettingsSO settingsSO;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private FixedJoint2D hookJoint;

        private float _targetDrag;
        private bool _haveHooked;

        public event Action OnHooked;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!CheckForTarget(col))
            {
                col.gameObject.TryGetComponent(out IDamageable target);
                target?.TakeDamage(0, gameObject.layer);
                return;
            }

            _haveHooked = true;
            hookJoint.connectedBody = col.rigidbody;
            hookJoint.enabled = true;
            
            OnHooked?.Invoke();
        }

        public void ShootSelf()
        {
            hookJoint.enabled = false;
            hookJoint.connectedBody = default;
            _haveHooked = false;
            rb.AddForce(transform.up * settingsSO.ShootOutForce, ForceMode2D.Impulse);
        }

        private bool CheckForTarget(Collision2D other)
        {
            return !_haveHooked && 
                   settingsSO.TargetMask.Contains(other.gameObject.layer) &&
                   other.gameObject.TryGetComponent(out IDraggable target) &&
                   target.IsDraggable;
        }
        
    }
}