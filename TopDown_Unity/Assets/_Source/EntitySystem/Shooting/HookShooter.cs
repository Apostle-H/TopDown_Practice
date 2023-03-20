using System;
using EntitySystem.Data.Combat;
using EntitySystem.Shooting.Projectiles;
using PlayerSystem.Interactions;
using UnityEngine;

namespace EntitySystem.Shooting
{
    public class HookShooter
    {
        private HookShooterSO _so;
        private readonly Transform _firePoint;
        private readonly Hook _hook;
        private readonly Dragger _dragger;

        private readonly Rigidbody2D _hookRb;

        private bool _hooked;

        public event Action OnHookOut;
        public event Action OnHooked;
        public event Action OnReleased;
        public event Action OnHookIn;

        public bool IsOut { get; private set; }

        public HookShooter(Transform firePoint, Hook hook, Dragger dragger, HookShooterSO so)
        {
            _so = so;
            _firePoint = firePoint;
            _hook = hook;
            _dragger = dragger;

            _hookRb = hook.GetComponent<Rigidbody2D>();

            _hook.OnHooked += Hooked;
        }
        
        public void ShootOut()
        {
            IsOut = true;
            _hooked = false;
            
            _hook.gameObject.SetActive(true);
            _hook.transform.position = _firePoint.position;
            _hook.transform.rotation = _firePoint.rotation;
            
            _dragger.Connect(_hookRb);
            _hook.ShootSelf();
            
            OnHookOut?.Invoke();
        }

        private void Hooked()
        {
            OnHooked?.Invoke();
            _hooked = true;
        }

        public void StoreIn()
        {
            _dragger.Release();
            _hook.gameObject.SetActive(false);
            IsOut = false;

            if (_hooked)
                OnReleased?.Invoke();
            OnHookIn?.Invoke();
        }
    }
}