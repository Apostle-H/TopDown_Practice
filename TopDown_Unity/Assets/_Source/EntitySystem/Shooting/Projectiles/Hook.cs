using System;
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
        [SerializeField] private FixedJoint2D enemyCarryJoint;

        private float _targetDrag;
        private bool _isReturning;

        public event Action OnReturn;

        private void OnTriggerExit2D(Collider2D other)
        {
            if (CheckOutOfRange(other))
            {
                GoBack();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (CheckForEnemyCollision(other) || CheckForWalls(other))
            {
                GoBack();
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

        private void Return()
        {
            if (enemyCarryJoint.connectedBody != null)
            {
                enemyCarryJoint.connectedBody.drag = _targetDrag;
                enemyCarryJoint.connectedBody = default;
            }
            enemyCarryJoint.enabled = false;
        }

        private bool CheckForEnemyCollision(Collider2D other)
        {
            if (!settingsSO.EnemyMask.Contains(other.gameObject.layer) ||
                enemyCarryJoint.connectedBody != null ||
                _isReturning)
            {
                return false;
            }

            #region JointSetting
            
            Rigidbody2D targetRb = other.GetComponent<Rigidbody2D>();
            targetRb.velocity = default;
            targetRb.angularVelocity = default;
            enemyCarryJoint.connectedBody = other.attachedRigidbody;
            _targetDrag = enemyCarryJoint.connectedBody.drag;
            enemyCarryJoint.connectedBody.drag = default;
            enemyCarryJoint.enabled = true;

            #endregion
            
            return true;
        }

        private bool CheckOutOfRange(Collider2D other) =>
            gameObject.activeSelf && other.gameObject.layer == gameObject.layer;

        private bool CheckForPlayerCollision(Collider2D other)
        {
            if (!_isReturning || !settingsSO.PlayerMask.Contains(other.gameObject.layer))
            {
                return false;
            }

            Return();
            OnReturn?.Invoke();
            return true;
        }

        private bool CheckForWalls(Collider2D other) =>
            !_isReturning && !settingsSO.PlayerMask.Contains(other.gameObject.layer) && other.gameObject.layer != gameObject.layer;
    }
}