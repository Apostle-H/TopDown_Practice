using System;
using PlayerSystem.Data.Shooting;
using UnityEngine;
using Utils;

namespace PlayerSystem.Shooting.Projectiles
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private HookSettingsSO settingsSO;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LayerMask playerMask;

        private bool _isReturning;
        
        public event Action OnReturn;
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (gameObject.activeSelf && other.gameObject.layer != gameObject.layer)
            {
                return;
            }
            
            Return();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!playerMask.Contains(col.gameObject.layer))
            {
                return;
            }
            
            Debug.Log(2);
            OnReturn?.Invoke();
        }

        public void ShootSelf()
        {
            _isReturning = false;
            rb.velocity = transform.up * settingsSO.Speed;
        }

        private void Return()
        {
            rb.velocity = transform.up * (settingsSO.Speed * -1);
            _isReturning = true;
        }
    }
}