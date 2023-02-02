using System;
using EntitySystem.Data.Combat;
using EntitySystem.Shooting.Projectiles;
using UnityEngine;

namespace EntitySystem.Shooting
{
    public class HookShooter
    {
        private HookShooterSettingsSO _settingsSO;
        private Transform _firePoint;
        private Hook _hook;

        private bool _isOut;

        public event Action OnHookOut;
        public event Action OnHookIn;
        
        public HookShooter(Transform firePoint, Hook hook, HookShooterSettingsSO settingsSO)
        {
            _settingsSO = settingsSO;
            _firePoint = firePoint;
            _hook = hook;

            _hook.OnReturn += StoreIn;
        }
        
        public void ShootOut()
        {
            if (_isOut)
            {
                return;
            }
            
            _hook.gameObject.SetActive(true);
            _hook.transform.position = _firePoint.position;
            _hook.transform.rotation = _firePoint.rotation;

            _hook.ShootSelf();
            
            _isOut = true;
            OnHookOut?.Invoke();
        }

        private void StoreIn()
        {
            _hook.gameObject.SetActive(false);

            _isOut = false;
            OnHookIn?.Invoke();
        }
    }
}