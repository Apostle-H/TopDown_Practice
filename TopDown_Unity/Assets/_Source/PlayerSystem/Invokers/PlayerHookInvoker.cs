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
        private InputHandler _input;

        private Transform _transform;
        private ShooterRotator _shooterRotator;
        private AreaCheckerSO _dragAreaCheckerSO;
        private Dragger _dragger;
        private HookShooter _hookShooter;

        public event Action OnHookOut;
        public event Action OnHookIn;
        
        public event Action OnHooked;
        public event Action OnReleased;

        public PlayerHookInvoker(InputHandler input, Transform transform, ShooterRotator shooterRotator,
            AreaCheckerSO dragAreaCheckerSO, Dragger dragger, HookShooter hookShooter)
        {
            _input = input;
            _transform = transform;
            _shooterRotator = shooterRotator;
            _dragAreaCheckerSO = dragAreaCheckerSO;
            _dragger = dragger;
            _hookShooter = hookShooter;
        }

        public void Bind()
        {
            _input.AttackActions.Hook.performed += Hook;
            _input.DragActions.ConnectRelease.performed += Drag;

            _hookShooter.OnHooked += Hooked;
            _hookShooter.OnReleased += Released;
        }

        public void Expose()
        {
            _input.AttackActions.Hook.performed -= Hook;
            _input.DragActions.ConnectRelease.performed -= Drag;

            _hookShooter.OnHooked -= Hooked;
            _hookShooter.OnReleased -= Released;
        }

        private void HookOut()
        {
            _input.DragActions.ConnectRelease.performed -= Drag;
            OnHookOut?.Invoke();
        }

        private void HookIn()
        {
            _input.DragActions.ConnectRelease.performed += Drag;
            OnHookIn?.Invoke();
        }

        private void Hooked() =>
            OnHooked?.Invoke();

        private void Released() =>
            OnReleased?.Invoke();
        
        private void Hook(InputAction.CallbackContext ctx)
        {
            if (!_hookShooter.IsOut)
            {
                _hookShooter.ShootOut();
                HookOut();
            }
            else
            {
                _hookShooter.StoreIn();
                HookIn();
            }
        }
        
        private void Drag(InputAction.CallbackContext ctx)
        {
            Rigidbody2D dragTarget;
            if (!_dragger.IsDragging && (dragTarget = PhysicsService.CheckArea(_transform, _dragAreaCheckerSO)))
            {
                float rotationAngle = _transform.LookAt2D(dragTarget.position).eulerAngles.z;
                _shooterRotator.Rotate(rotationAngle);
                Hook(default);
            }
        }
    }
}