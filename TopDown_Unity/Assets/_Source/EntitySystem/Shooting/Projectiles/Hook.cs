using System;
using System.Collections;
using System.IO;
using DG.Tweening;
using EntitySystem.Data.Combat.Projectiles;
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

        private bool CheckForTarget(Collision2D other) =>
            settingsSO.TargetMask.Contains(other.gameObject.layer) && !_haveHooked;
        
    }
}