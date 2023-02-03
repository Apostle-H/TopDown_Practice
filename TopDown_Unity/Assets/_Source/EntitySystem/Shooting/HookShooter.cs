using System;
using EntitySystem.Data.Combat;
using EntitySystem.Shooting.Projectiles;
using PlayerSystem.Interactions;
using UnityEngine;

namespace EntitySystem.Shooting
{
    public class HookShooter
    {
        private HookShooterSettingsSO _settingsSO;
        private Transform _firePoint;
        private Hook _hook;
        private Dragger _dragger;

        private Rigidbody2D _hookRb;

        public bool IsOut { get; private set; }

        public HookShooter(Transform firePoint, Hook hook, Dragger dragger, HookShooterSettingsSO settingsSO)
        {
            _settingsSO = settingsSO;
            _firePoint = firePoint;
            _hook = hook;
            _dragger = dragger;

            _hookRb = hook.GetComponent<Rigidbody2D>();
        }
        
        public void ShootOut()
        {
            IsOut = true;
            _hook.gameObject.SetActive(true);
            _hook.transform.position = _firePoint.position;
            _hook.transform.rotation = _firePoint.rotation;
            
            _dragger.Connect(_hookRb);
            _hook.ShootSelf();
        }

        public void StoreIn()
        {
            _dragger.Release();
            _hook.gameObject.SetActive(false);
            IsOut = false;
        }
    }
}