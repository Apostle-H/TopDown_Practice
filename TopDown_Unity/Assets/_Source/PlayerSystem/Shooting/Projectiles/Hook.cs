using System;
using System.IO;
using PlayerSystem.Data.Shooting;
using UnityEngine;
using Utils;

namespace PlayerSystem.Shooting.Projectiles
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private HookSettingsSO settingsSO;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private FixedJoint2D enemyCarryJoint;

        private bool _isReturning;

        public event Action OnReturn;
        
        private void OnTriggerExit2D(Collider2D other)
        {
            CheckOutOfRange(other);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (CheckForEnemyCollision(other))
            {
                return;
            }
            
            CheckForPlayerCollision(other);
        }

        public void ShootSelf()
        {
            _isReturning = false;
            rb.velocity = transform.up * settingsSO.Speed;
        }

        private void GoBack()
        {
            rb.velocity = transform.up * (settingsSO.Speed * -1);
            _isReturning = true;
        }

        private bool CheckForEnemyCollision(Collider2D other)
        {
            if (!settingsSO.EnemyMask.Contains(other.gameObject.layer) ||
                enemyCarryJoint.connectedBody != null)
            {
                return false;
            }

            #region JointSetting
            
            Rigidbody2D targetRb = other.GetComponent<Rigidbody2D>();
            targetRb.velocity = default;
            targetRb.angularVelocity = default;
            enemyCarryJoint.connectedBody = other.attachedRigidbody;
            enemyCarryJoint.enabled = true;
            
            #endregion
            
            GoBack();
            return true;
        }

        private bool CheckOutOfRange(Collider2D other)
        {
            if (gameObject.activeSelf && other.gameObject.layer != gameObject.layer)
            {
                return false;
            }
            
            GoBack();
            return true;
        }

        private void CheckForPlayerCollision(Collider2D other)
        {
            if (!settingsSO.PlayerMask.Contains(other.gameObject.layer))
            {
                return;
            }

            enemyCarryJoint.connectedBody = default;
            enemyCarryJoint.enabled = false;
            
            OnReturn?.Invoke();
        }
    }
}