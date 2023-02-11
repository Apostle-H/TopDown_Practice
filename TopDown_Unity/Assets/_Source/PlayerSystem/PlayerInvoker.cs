using System;
using EntitySystem.Health;
using InputSystem;
using PlayerSystem.Interactions;
using EntitySystem.Shooting;
using EntitySystem.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace PlayerSystem
{
    public class PlayerInvoker
    {
        private InputHandler _input;

        private Transform _transform;
        private PlayerHealth _health;
        private Mover _mover;
        private ShooterRotator _shooterRotator;
        private Attacker _attacker;
        private AreaChecker _dragAreaChecker;
        private Dragger _dragger;
        private HookShooter _hookShooter;

        public PlayerInvoker(InputHandler input, Transform transform, PlayerHealth health,
            Mover mover, ShooterRotator shooterRotator, Attacker attacker, AreaChecker dragAreaChecker,
            Dragger dragger, HookShooter hookShooter)
        {
            _input = input;
            _transform = transform;
            _health = health;
            _mover = mover;
            _shooterRotator = shooterRotator;
            _attacker = attacker;
            _dragAreaChecker = dragAreaChecker;
            _dragger = dragger;
            _hookShooter = hookShooter;
        }

        public void Bind()
        {
            _health.OnDeath += Die;
            
            _input.MovementActions.Direction.performed += UpdateDirection;
            _input.MovementActions.Direction.canceled += UpdateDirection;
            _input.AttackActions.MousePos.performed += RotateGun;
            _input.AttackActions.Shoot.started += SetShoot;
            _input.AttackActions.Shoot.canceled += SetShoot;
            _input.AttackActions.Hook.performed += Hook;
            _input.DragActions.ConnectRelease.performed += Drag;

            _hookShooter.OnHooked += Hooked;
            _hookShooter.OnReleased += HookReleased;
        }

        public void Expose()
        {
            _input.MovementActions.Direction.performed -= UpdateDirection;
            _input.MovementActions.Direction.canceled -= UpdateDirection;
            _input.AttackActions.MousePos.performed -= RotateGun;
            _input.AttackActions.Shoot.started -= SetShoot;
            _input.AttackActions.Shoot.canceled -= SetShoot;
            _input.AttackActions.Hook.performed -= Hook;
            _input.DragActions.ConnectRelease.performed -= Drag;

            _hookShooter.OnHooked -= Hooked;
            _hookShooter.OnReleased -= HookReleased;
        }

        private void HookOut()
        {
            _input.AttackActions.Shoot.started -= SetShoot;
            _input.DragActions.ConnectRelease.performed -= Drag;
            
            _attacker.StopShoot();
        }
        
        private void HookIn()
        {
            _input.AttackActions.Shoot.performed += SetShoot;
            _input.DragActions.ConnectRelease.performed += Drag;
        }

        private void Hooked()
        {
            _mover.UpdateIsCarrying(true);
        }
        
        private void HookReleased()
        {
            _mover.UpdateIsCarrying(false);
        }

        private void UpdateDirection(InputAction.CallbackContext ctx) =>
            _mover.UpdateDirection(ctx.ReadValue<Vector2>());

        private void RotateGun(InputAction.CallbackContext ctx)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            float rotationAngle = _transform.LookAt2D(mousePos).eulerAngles.z;
            _shooterRotator.Rotate(rotationAngle);
        }

        private void SetShoot(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Started)
            {
                _attacker.StartShoot();
            }
            else if (ctx.phase == InputActionPhase.Canceled)
            {
                _attacker.StopShoot();
            }
        }

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
            if (!_dragger.IsDragging && (dragTarget = _dragAreaChecker.CheckArea()))
            {
                float rotationAngle = _transform.LookAt2D(dragTarget.position).eulerAngles.z;
                _shooterRotator.Rotate(rotationAngle);
                Hook(default);
            }
        }

        private void Die()
        {
            Expose();
            
            _mover.UpdateDirection(Vector2.zero);
            _attacker.StopShoot();
        }
    }
}