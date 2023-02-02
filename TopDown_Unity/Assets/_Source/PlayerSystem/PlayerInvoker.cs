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
        private HookShooter _hookShooter;
        private Dragger _dragger;

        public PlayerInvoker(InputHandler input, Transform transform, Mover mover, 
            ShooterRotator shooterRotator, Attacker attacker, HookShooter hookShooter,
            Dragger dragger)
        {
            _input = input;
            _transform = transform;
            _mover = mover;
            _shooterRotator = shooterRotator;
            _attacker = attacker;
            _hookShooter = hookShooter;
            _dragger = dragger;
            
            _hookShooter.OnHookOut += BlockControls;
            _hookShooter.OnHookIn += Bind;
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
            ExposeMovement();
            ExposeAttack();
            ExposeDrag();
        }

        private void BlockControls()
        {
            _mover.UpdateDirection(Vector2.zero);
            
            ExposeMovement();
            ExposeAttack();
            ExposeDrag();
        }

        private void ExposeMovement()
        {
            _input.MovementActions.Direction.performed -= UpdateDirection;
            _input.MovementActions.Direction.canceled -= UpdateDirection;
        }

        private void ExposeAttack()
        {
            _input.AttackActions.MousePos.performed -= RotateGun;
            _input.AttackActions.Shoot.performed -= Shoot;
            _input.AttackActions.Hook.performed -= Hook;
        }

        private void ExposeDrag()
        {
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

        private void Hook(InputAction.CallbackContext ctx) =>
            _hookShooter.ShootOut();
        
        private void Drag(InputAction.CallbackContext ctx)
        {
            if (_dragger.IsDragging)
            {
                _dragger.Release();
            }
            else
            {
                _dragger.Connect();
            }
        }
    }
}