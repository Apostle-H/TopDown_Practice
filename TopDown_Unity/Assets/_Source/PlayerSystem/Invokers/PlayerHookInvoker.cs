using System;
using EntitySystem.Shooting;
using InputSystem;
using PlayerSystem.Data.Interactions;
using PlayerSystem.Interactions;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Utils.Services;

namespace PlayerSystem.Invokers
{
    public class PlayerHookInvoker
    {
        private readonly InputHandler _input;

        private readonly Transform _transform;
        private readonly Rotator _rotator;
        private readonly AreaCheckerSO _dragAreaCheckerSO;
        private readonly Dragger _dragger;
        private readonly HookShooter _hookShooter;

        public event Action OnHookOut;
        public event Action OnHooked;
        public event Action OnReleased;
        public event Action OnHookIn;

        public PlayerHookInvoker(InputHandler input, Transform transform, Rotator rotator,
            AreaCheckerSO dragAreaCheckerSO, Dragger dragger, HookShooter hookShooter)
        {
            _input = input;
            _transform = transform;
            _rotator = rotator;
            _dragAreaCheckerSO = dragAreaCheckerSO;
            _dragger = dragger;
            _hookShooter = hookShooter;
        }

        public void Bind()
        {
            _input.ShootActions.Hook.performed += Hook;
            _input.InteractionActions.ConnectRelease.performed += Drag;

            _hookShooter.OnHookOut += HookOut;
            _hookShooter.OnHooked += Hooked;
            _hookShooter.OnReleased += Released;
            _hookShooter.OnHookIn += HookIn;
        }

        public void Expose()
        {
            _input.ShootActions.Hook.performed -= Hook;
            _input.InteractionActions.ConnectRelease.performed -= Drag;

            _hookShooter.OnHookOut -= HookOut;
            _hookShooter.OnHooked -= Hooked;
            _hookShooter.OnReleased -= Released;
            _hookShooter.OnHookIn -= HookIn;
        }

        private void HookOut()
        {
            _input.InteractionActions.ConnectRelease.performed -= Drag;
            OnHookOut?.Invoke();
        }

        private void HookIn()
        {
            _input.InteractionActions.ConnectRelease.performed += Drag;
            OnHookIn?.Invoke();
        }

        private void Hooked() =>
            OnHooked?.Invoke();

        private void Released() =>
            OnReleased?.Invoke();
        
        private void Hook(InputAction.CallbackContext ctx)
        {
            if (!_hookShooter.IsOut)
                _hookShooter.ShootOut();
            else
                _hookShooter.StoreIn();
        }
        
        private void Drag(InputAction.CallbackContext ctx)
        {
            Rigidbody2D dragTarget;
            if (!_dragger.IsDragging && (dragTarget = PhysicsService.CheckArea(_transform, _dragAreaCheckerSO)))
            {
                float rotationAngle = _transform.LookAt2D(dragTarget.position).eulerAngles.z;
                _rotator.Rotate(rotationAngle);
                Hook(default);
            }
        }
    }
}