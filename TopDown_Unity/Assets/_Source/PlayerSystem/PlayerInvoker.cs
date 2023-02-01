using InputSystem;
using PlayerSystem.Shooting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem.Movement
{
    public class PlayerInvoker
    {
        private InputHandler _input;

        private Transform _transform;
        private Mover _mover;
        private ShooterRotator _shooterRotator;
        private Shooter _shooter;
        private HookShooter _hookShooter;

        public PlayerInvoker(InputHandler input, Transform transform, Mover mover, ShooterRotator shooterRotator, Shooter shooter, HookShooter hookShooter)
        {
            _input = input;
            _transform = transform;
            _mover = mover;
            _shooterRotator = shooterRotator;
            _shooter = shooter;
            _hookShooter = hookShooter;
            
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
        }

        public void Expose()
        {
            _input.MovementActions.Direction.performed -= UpdateDirection;
            _input.MovementActions.Direction.canceled -= UpdateDirection;
            _input.AttackActions.MousePos.performed -= RotateGun;
            _input.AttackActions.Shoot.performed -= Shoot;
            _input.AttackActions.Hook.performed -= Hook;
        }

        private void BlockControls()
        {
            _mover.UpdateDirection(Vector2.zero);
            Expose();
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
            _shooter.Shoot();

        private void Hook(InputAction.CallbackContext ctx) =>
            _hookShooter.ShootOut();
    }
}