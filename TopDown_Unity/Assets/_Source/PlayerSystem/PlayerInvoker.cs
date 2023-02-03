using System;
using InputSystem;
using PlayerSystem.Interactions;
using EntitySystem.Shooting;
using EntitySystem.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem
{
    public class PlayerInvoker
    {
        private InputHandler _input;

        private Transform _transform;
        private Mover _mover;
        private ShooterRotator _shooterRotator;
        private Attacker _attacker;
        private AreaChecker _dragAreaChecker;
        private Dragger _dragger;
        private HookShooter _hookShooter;

        public PlayerInvoker(InputHandler input, Transform transform, Mover mover, 
            ShooterRotator shooterRotator, Attacker attacker, AreaChecker dragAreaChecker,
            Dragger dragger, HookShooter hookShooter)
        {
            _input = input;
            _transform = transform;
            _mover = mover;
            _shooterRotator = shooterRotator;
            _attacker = attacker;
            _dragAreaChecker = dragAreaChecker;
            _dragger = dragger;
            _hookShooter = hookShooter;
        }

        public void Bind()
        {
            _input.MovementActions.Direction.performed += UpdateDirection;
            _input.MovementActions.Direction.canceled += UpdateDirection;
            _input.AttackActions.MousePos.performed += RotateGun;
            _input.AttackActions.Shoot.performed += Shoot;
            _input.AttackActions.Hook.performed += Hook;
            _input.DragActions.ConnectRelease.performed += Drag;
        }

        public void Expose()
        {
            _input.MovementActions.Direction.performed -= UpdateDirection;
            _input.MovementActions.Direction.canceled -= UpdateDirection;
            _input.AttackActions.MousePos.performed -= RotateGun;
            _input.AttackActions.Shoot.performed -= Shoot;
            _input.AttackActions.Hook.performed -= Hook;
            _input.DragActions.ConnectRelease.performed -= Drag;
        }

        private void BindOnHookIn()
        {
            _input.AttackActions.Shoot.performed += Shoot;
            _input.DragActions.ConnectRelease.performed += Drag;
        }
        
        private void ExposeOnHookOut()
        {
            _input.AttackActions.Shoot.performed -= Shoot;
            _input.DragActions.ConnectRelease.performed -= Drag;
        }

        private void UpdateDirection(InputAction.CallbackContext ctx) =>
            _mover.UpdateDirection(ctx.ReadValue<Vector2>());

        private void RotateGun(InputAction.CallbackContext ctx)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            float rotationAngle = Mathf.Atan2(mousePos.y - _transform.position.y, mousePos.x - _transform.position.x) * (180/Mathf.PI) - 90;
            _shooterRotator.Rotate(rotationAngle);
        }
        
        private void Shoot(InputAction.CallbackContext ctx) =>
            _attacker.Shoot();

        private void Hook(InputAction.CallbackContext ctx)
        {
            if (!_hookShooter.IsOut)
            {
                _hookShooter.ShootOut();
                ExposeOnHookOut();
            }
            else
            {
                _hookShooter.StoreIn();
                BindOnHookIn();
            }
        }
        
        private void Drag(InputAction.CallbackContext ctx)
        {
            Rigidbody2D dragTarget;
            
            if (!_dragger.IsDragging && (dragTarget = _dragAreaChecker.CheckArea()))
            {
                _dragger.Connect(dragTarget);
            }
            else if (_dragger.IsDragging)
            {
                _dragger.Release();
            }
        }
    }
}